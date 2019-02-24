using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace kbinxml_sharp
{
    public class KbinReader
    {
        public KbinReader(string filename) : this(File.ReadAllBytes(filename))
        {

        }

        public KbinReader(byte[] data)
        {
            this.data = data;
        }

        private void GetHeader() 
        {
            int nodeLength = data.Slice(4, 8).ToInt32();
            bool compressed = data[1] == 0x42;

            nodeBuffer = new KbinNodeBuffer(data.Slice(8, 8 + nodeLength), compressed, GetEncoding());

            int dataStart = nodeLength + 12;
            int dataLength = data.Slice(dataStart - 4, dataStart).ToInt32();

            dataBuffer = new KbinDataBuffer(data.Slice(dataStart, dataStart + dataLength), GetEncoding());
        }

        private string GetEncoding()
        {
            int flag = data[2] >> 5;
            return Encodings.encodings[flag];
        }

        public XmlDocument XmlFromBinary()
        {
            GetHeader();
            nodeBuffer.Reset(); dataBuffer.Reset();
            XmlDocument xml = new XmlDocument();
            XmlNode currentNode = null;
            bool fileEnd = false;

            XmlDeclaration declaration = xml.CreateXmlDeclaration
                ("1.0", GetEncoding(), null);

            xml.InsertBefore(declaration, xml.DocumentElement);

            while (!fileEnd)
            {
                byte current = nodeBuffer.ReadU8();
                int actual = current & ~(1 << 6);
                ControlTypes controlTypes;
                ConstructTypes valueTypes;

                bool hasControlType = Types.ControlTypeMap.
                    TryGetValue(actual, out controlTypes);

                bool hasValueType = Types.ConstructTypeMap.
                    TryGetValue(actual, out valueTypes);

                if (hasControlType)
                {
                    switch (controlTypes)
                    {
                        case ControlTypes.NodeStart: 
                            string name = nodeBuffer.ReadString();

                            XmlElement newNode = xml.CreateElement(name);
                            if (currentNode != null)
                            {
                                currentNode.AppendChild(newNode);
                            }
                            else
                            {
                                xml.AppendChild(newNode);
                            }
                            currentNode = newNode;
                            break;
                        case ControlTypes.Attribute: 
                            string attributeName = nodeBuffer.ReadString();

                            int valueLength = (int)dataBuffer.ReadU32();
                            string attributeValue = dataBuffer.ReadString(valueLength);

                            XmlAttribute attr = xml.CreateAttribute(attributeName);
                            attr.Value = attributeValue;

                            currentNode.Attributes.Append(attr);
                            break;
                        case ControlTypes.NodeEnd: 
                            if (currentNode.ParentNode != null)
                            {
                                currentNode = currentNode.ParentNode;
                            }
                            break;
                        case ControlTypes.FileEnd:
                            fileEnd = true;
                            return xml;
                    }
                }
                else if (hasValueType)
                {
                    string valueName = valueTypes.name;
                    bool isArray = ((current >> 6) & 1) == 1 | valueTypes.size == -1;
                    string nodeName = nodeBuffer.ReadString();
                    int arraySize = valueTypes.size;

                    if (isArray) arraySize = (int)dataBuffer.ReadU32();

                    XmlElement newNode = xml.CreateElement(nodeName);
                    XmlAttribute newAttr = xml.CreateAttribute("__type");
                    currentNode.AppendChild(newNode);
                    newNode.Attributes.Append(newAttr);
                    newAttr.Value = valueName;

                    int numElements = arraySize / valueTypes.size;

                    switch (valueName)
                    {
                        case "bin":
                            newAttr = xml.CreateAttribute("__size");
                            newAttr.Value = arraySize.ToString();
                            newNode.Attributes.Append(newAttr);

                            byte[] bytes = dataBuffer.ReadFrom4Byte(arraySize);
                            newNode.InnerText = BitConverter.ToString(bytes).Replace("-", "");
                            break;
                        case "str":
                            newNode.InnerText = dataBuffer.ReadString(arraySize);
                            break;
                        default:
                            if (isArray)
                            {
                                newAttr = xml.CreateAttribute("__count");
                                newAttr.Value = numElements.ToString();
                                newNode.Attributes.Append(newAttr);
                            }

                            byte[] byteList = new byte[0];

                            if (numElements == 1 && valueTypes.count > 1)
                            {
                                numElements = valueTypes.count;
                                byteList = dataBuffer.ReadBytes(numElements * valueTypes.size);
                            }
                            else
                            {
                                byteList = dataBuffer.ReadBytes(arraySize);
                            }

                            List<string> stringList = new List<string>();

                            for (int i = 0; i < numElements; i++)
                            {
                                byte[] arrayBytes = byteList.Slice(i * valueTypes.size, (i + 1) * valueTypes.size);
                                ByteConv byteConv = new ByteConv(valueTypes);
                                stringList.Add(byteConv.ConvertBytes(arrayBytes));
                            }
                            newNode.InnerText = string.Join(" ", stringList.ToArray().Reverse());
                            break;
                    }
                    currentNode = newNode;
                }
            }
            return new XmlDocument();
        }

        private byte[] data;
        private KbinNodeBuffer nodeBuffer;
        private KbinDataBuffer dataBuffer;
    }
}
