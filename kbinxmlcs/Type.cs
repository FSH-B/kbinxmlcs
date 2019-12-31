using static kbinxmlcs.Converters;

namespace kbinxmlcs
{
    public class NodeType
    {
        public int Size 
        {
            get;
        }
        
        public int Count
        {
            get; 
        }

        public string Name
        {
            get;
        }

        public StringConverter ToBytes
        {
            get;
        }

        public new ByteConverter ToString
        {
            get;
        }

        public NodeType(int size, int count, string name, 
            StringConverter stringConverter, ByteConverter byteConverter)
        {
            Size = size;
            Count = count;
            Name = name;
            ToBytes = stringConverter;
            ToString = byteConverter;
        }
    }
}