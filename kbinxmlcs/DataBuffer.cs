using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace kbinxmlcs
{
    internal class DataBuffer : BigEndianBinaryBuffer
    {
        private Encoding _encoding;
        private int _pos32, _pos16, _pos8;

        internal DataBuffer(byte[] buffer, Encoding encoding)
        {
            Buffer = buffer.ToList();
            _encoding = encoding;
        }

        internal DataBuffer(Encoding encoding)
        {
            _encoding = encoding;
        }

        private void Realign16_8()
        {
            if (_pos8 % 4 == 0)
                _pos8 = _pos32;

            if (_pos16 % 4 == 0)
                _pos16 = _pos32;
        }

        internal byte[] Read32BitAligned(int count)
        {
            byte[] result = Buffer.Skip(_pos32).Take(count).ToArray();
            while (count % 4 != 0)
                count++;
            _pos32 += count;

            Realign16_8();

            return result;
        }

        internal byte[] Read16BitAligned()
        {
            if (_pos16 % 4 == 0)
                _pos32 += 4;

            byte[] result = Buffer.Skip(_pos16).Take(2).ToArray();
            _pos16 += 2;
            Realign16_8();

            return result;
        }

        internal byte[] Read8BitAligned()
        {
            if (_pos8 % 4 == 0)
                _pos32 += 4;

            byte[] result = Buffer.Skip(_pos8).Take(1).ToArray();
            _pos8++;
            Realign16_8();

            return result;
        }

        public void Write32BitAligned(byte[] buffer)
        {
            while (_pos32 > Buffer.Count())
                Buffer.Add(0);

            SetRange(buffer, ref _pos32);
            while (_pos32 % 4 != 0)
                _pos32++;

            Realign16_8();
        }

        internal void Write16BitAligned(byte[] buffer)
        {
            while (_pos16 > Buffer.Count())
                Buffer.Add(0);

            if (_pos16 % 4 == 0)
                _pos32 += 4;

            SetRange(buffer, ref _pos16);
            Realign16_8();
        }

        internal void Write8BitAligned(byte value)
        {
            while (_pos8 > Buffer.Count())
                Buffer.Add(0);

            if (_pos8 % 4 == 0)
                _pos32 += 4;

            SetRange(new byte[] { value }, ref _pos8);
            Realign16_8();
        }

        internal override byte[] ReadBytes(int count)
        {
            switch (count)
            {
                case 1:
                    return Read8BitAligned();

                case 2:
                    return Read16BitAligned();

                default:
                    return Read32BitAligned(count);
            }
        }

        internal override void WriteBytes(byte[] buffer)
        {
            switch (buffer.Length)
            {
                case 1:
                    Write8BitAligned(buffer[0]);
                    break;

                case 2:
                    Write16BitAligned(buffer);
                    break;

                default:
                    Write32BitAligned(buffer);
                    break;
            }
        }

        internal void WriteString(string value)
        {
            var buffer = new List<byte>(_encoding.GetBytes(value));
            buffer.Add(0);
            WriteU32((uint)buffer.Count);
            Write32BitAligned(buffer.ToArray());
        }

        internal string ReadString(int count) => _encoding.GetString(Read32BitAligned(count)).TrimEnd('\0');

       private static byte[] ConvertHexString(string hexString) => Enumerable.Range(0, hexString.Length).Where(x => x % 2 == 0)
            .Select(x => byte.Parse(hexString.Substring(x, 2), NumberStyles.HexNumber)).ToArray();

        internal void WriteBinary(string value)
        {
            WriteU32((uint)value.Length / 2);
            Write32BitAligned(ConvertHexString(value));
        }

        internal string ReadBinary(int count) => BitConverter.ToString(Read32BitAligned(count)).Replace("-", "").ToLower();

        private void SetRange(byte[] buffer, ref int offset)
        {
            if (offset == Buffer.Count())
            {
                Buffer.InsertRange(offset, buffer);
                offset += buffer.Length;
            }
            else
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    Buffer[offset] = buffer[i];
                    offset++;
                }
            }
        }
    }
}
