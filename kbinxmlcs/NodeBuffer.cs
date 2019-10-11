using System;
using System.Text;

namespace kbinxmlcs
{
    internal class NodeBuffer : BigEndianBinaryBuffer
    {
        private Encoding _encoding;
        private bool _compressed;

        internal NodeBuffer(bool compressed, Encoding encoding)
            : base()
        {
            _compressed = compressed;
            _encoding = encoding;
        }

        internal NodeBuffer(byte[] buffer, bool compressed, Encoding encoding)
            : base(buffer)
        {
            _compressed = compressed;
            _encoding = encoding;
        }

        internal void WriteString(string value)
        {
            if (_compressed)
            {
                WriteU8((byte)value.Length);
                WriteBytes(Sixbit.Encode(value));
            }
            else
            {
                WriteU8((byte)((value.Length - 1) | (1 << 6)));
                WriteBytes(_encoding.GetBytes(value));
            }
        }

        internal string ReadString()
        {
            int length = ReadU8();

            if (_compressed)
                return Sixbit.Decode(ReadBytes((int)Math.Ceiling(length * 6 / 8.0)), length);
            else
                return _encoding.GetString(ReadBytes((length & 0b10111111) + 1));
        }
    }
}
