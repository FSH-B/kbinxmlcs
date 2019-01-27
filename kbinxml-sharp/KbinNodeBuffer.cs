using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal class KbinNodeBuffer
    {
        public KbinNodeBuffer(byte[] data, bool compressed, string encoding)
        {
            this.data = data;
            this.compressed = compressed;
            this.encoding = encoding;
        }

        public void Reset()
        {
            index = 0;
        }

        public byte[] ReadBytes(int num)
        {
            byte[] result = data.Slice(index, index + num);
            index += num;
            return result;
        }

        public byte ReadU8()
        {
            byte result = ReadBytes(1)[0];
            return result;
        }

        public string ReadString()
        {
            int length = ReadU8();
            if (compressed)
            {
                double toRead = Math.Ceiling(length * 6 / 8.0);
                byte[] nameBytes = ReadBytes((int)toRead);
                return nameBytes.Unpack(length);
            }
            else
            {
                byte[] readBytes = ReadBytes((length & ~64) + 1);
                return Encoding.GetEncoding(encoding).GetString(readBytes);
            }
        }

        private byte[] data;
        private bool compressed;
        private string encoding;
        private int index = 0;
    }
}
