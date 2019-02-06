using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal class KbinDataBuffer
    {
        public KbinDataBuffer(byte[] data, string encoding)
        {
            this.data = data;
            this.encoding = encoding;
        }

        public void Reset()
        {
            pos8 = 0; pos16 = 0; pos32 = 0;
        }

        public byte[] ReadBytes(int num)
        {
            byte[] result;

            if (num == 1)
            {
                result = BitConverter.GetBytes(data[pos8]);
            }
            else if (num == 2)
            {
                result = data.Slice(pos16, pos16 + 2);
            }
            else if (num >= 3)
            {
                result = data.Slice(pos32, pos32 + num);
            }
            else
            {
                return new byte[0];
            }

            Realign(num);

            Array.Reverse(result);
            return result;
        }

        void Realign(int bytesRead)
        {
            if (bytesRead == 1)
            {
                if (pos8 % 4 == 0)
                {
                    pos32 += 4;
                }
                pos8++;
            }
            else if (bytesRead == 2)
            {
                if (pos16 % 4 == 0)
                {
                    pos32 += 4;
                }
                pos16 += 2;
            }
            else if (bytesRead >= 3)
            {
                var newNum = bytesRead;
                if (newNum % 4 != 0)
                {
                    newNum += 4 - (newNum % 4);
                }
                pos32 += newNum;
            }
            if (pos8 % 4 == 0)
            {
                pos8 = pos32;
            }
            if (pos16 % 4 == 0)
            {
                pos16 = pos32;
            }
        }

        void Realign4Byte(int num)
        {
            if (num % 4 != 0)
            {
                Realign(num + 4 - (num % 4));
            }
            else
            {
                Realign(num);
            }
        }

        public byte ReadU8()
        {
            var result = ReadBytes(1)[0];
            return result;
        }

        public ushort ReadU16()
        {
            var result = ReadBytes(2);
            return BitConverter.ToUInt16(result, 0);
        }

        public uint ReadU32()
        {
            var result = ReadBytes(4);
            return BitConverter.ToUInt32(result, 0);
        }

        public byte[] ReadFrom4Byte(int num)
        {
            if (num == 0) return new byte[0];
            var read = data.Slice(pos32, pos32 + num);
            Realign4Byte(num);
            return read;
        }

        public string ReadString(int length)
        {
            var readBytes = ReadFrom4Byte(length);
            if (readBytes.Last() == 0x00)
            {
                Array.Resize(ref readBytes, readBytes.Length - 1);
            }
            return Encoding.GetEncoding(encoding).GetString(readBytes);
        }

        private int pos8, pos16, pos32 = 0;
        private string encoding;
        private byte[] data;
    }
}
