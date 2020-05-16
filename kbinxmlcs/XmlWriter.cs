using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace kbinxmlcs
{
    /// <summary>
    /// Represents a writer for Konami's binary XML format.
    /// </summary>
    public class XmlWriter
    {
        private readonly XmlDocument _document;
        private readonly Encoding _encoding;

        private readonly NodeBuffer _nodeBuffer;
        private readonly DataBuffer _dataBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="xmlDocument">The XML document to be wirtten as a binary XML.</param>
        /// <param name="encoding">The encoding of the XML document.</param>
        public XmlWriter(XmlDocument document, Encoding encoding)
        {
            _document = document;
            _encoding = encoding;
            AlphabetizeAttributes(_document.DocumentElement);

            _nodeBuffer = new NodeBuffer(true, encoding);
            _dataBuffer = new DataBuffer(encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="xNode">The XML document to be wirtten as a binary XML.</param>
        /// <param name="encoding">The encoding of the XML document.</param>
        public XmlWriter(XNode node, Encoding encoding)
        {
            _document = new XmlDocument();
            _document.LoadXml(node.ToString());
            _encoding = encoding;
            AlphabetizeAttributes(_document.DocumentElement);

            _nodeBuffer = new NodeBuffer(true, encoding);
            _dataBuffer = new DataBuffer(encoding);

        }

        /// <summary>
        /// Writes all nodes in the XML document.
        /// </summary>
        /// <returns>Retruns an array of bytes containing the contents of the binary XML.</returns>
        public byte[] Write()
        {
            Recurse(_document.DocumentElement);
            _nodeBuffer.WriteU8(255);
            _nodeBuffer.Pad();
            _dataBuffer.Pad();

            //Write header data
            var output = new BigEndianBinaryBuffer();
            output.WriteU8(0xA0); //Magic
            output.WriteU8(0x42); //Compression flag
            output.WriteU8(EncodingDictionary.ReverseEncodingMap[_encoding]);
            output.WriteU8((byte)~EncodingDictionary.ReverseEncodingMap[_encoding]);

            //Write node buffer length and contents.
            output.WriteS32(_nodeBuffer.ToArray().Length);
            output.WriteBytes(_nodeBuffer.ToArray());

            //Write data buffer length and contents.
            output.WriteS32(_dataBuffer.ToArray().Length);
            output.WriteBytes(_dataBuffer.ToArray());

            return output.ToArray();
        }

        private void Recurse(XmlElement xmlElement)
        {
            var typestr = xmlElement.Attributes["__type"]?.Value;
            var sizestr = xmlElement.Attributes["__count"]?.Value;

            if (typestr == null)
            {
                _nodeBuffer.WriteU8(1);
                _nodeBuffer.WriteString(xmlElement.Name);
            }
            else
            {
                var typeid = TypeDictionary.ReverseTypeMap[typestr];
                if (sizestr != null)
                    _nodeBuffer.WriteU8((byte)(typeid | 0x40));
                else
                    _nodeBuffer.WriteU8(typeid);

                _nodeBuffer.WriteString(xmlElement.Name);
                if (typestr == "str")
                    _dataBuffer.WriteString(xmlElement.InnerText);
                else if (typestr == "bin")
                    _dataBuffer.WriteBinary(xmlElement.InnerText);
                else
                {
                    var type = TypeDictionary.TypeMap[typeid];
                    var value = xmlElement.InnerText.Split(' ');
                    var size = (uint)(type.Size * type.Count);

                    if (sizestr != null)
                    {
                        size *= uint.Parse(sizestr);
                        _dataBuffer.WriteU32(size);
                    }

                    var values = new List<byte>();
                    
                    for (var i = 0; i < size / type.Size; i++)
                        values.AddRange(type.ToBytes(value[i]));
                    
                    _dataBuffer.WriteBytes(values.ToArray());
                }
            }

            foreach (XmlAttribute attribute in xmlElement.Attributes)
            {
                if (attribute.Name != "__type" &&
                    attribute.Name != "__size" &&
                    attribute.Name != "__count")
                {
                    _nodeBuffer.WriteU8(0x2E);
                    _nodeBuffer.WriteString(attribute.Name);
                    _dataBuffer.WriteString(attribute.Value);
                }
            }

            foreach (XmlNode childNode in xmlElement.ChildNodes)
            {
                if (childNode is XmlElement)
                    Recurse((XmlElement)childNode);
            }
            _nodeBuffer.WriteU8(0xFE);
        }

        private void AlphabetizeAttributes(XmlElement element)
        {
            var attributes = element.Attributes.Cast<XmlAttribute>()
                .Where(x => x.Name != "__type" && x.Name != "__size" && x.Name != "__count")
                .OrderBy(x => x.Name);
            foreach(var attribute in attributes)
            {
                element.Attributes.Append(attribute);
            }

            foreach (XmlNode child in element.ChildNodes)
            {
                if (child is XmlElement)
                    AlphabetizeAttributes((XmlElement)child);
            }
        }
    }
}
