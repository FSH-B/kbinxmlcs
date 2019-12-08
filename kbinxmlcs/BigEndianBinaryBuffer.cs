using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kbinxmlcs
{
    internal class BigEndianBinaryBuffer
    {
        protected bool isRead;
        protected List<byte> Buffer;
        protected byte[] Buffer_ro;
        protected int Offset = 0;

        internal BigEndianBinaryBuffer(byte[] buffer)
        {
            isRead = true;
            Buffer_ro = buffer;
            Buffer = new List<byte>(buffer);
        }

        internal BigEndianBinaryBuffer()
        {
            isRead = false;
            Buffer = new List<byte>();
        }

        internal virtual byte[] ReadBytes(int count)
        {
            byte[] buffer;
            if (isRead)
            {
                buffer = new byte[count];
                System.Buffer.BlockCopy(Buffer_ro, Offset, buffer, 0, count);
            }
            else
            {
                if (count == 1)
                {
                    buffer = new byte[] { Buffer[Offset] };
                }
                else
                {
                    buffer = Buffer.Skip(Offset).Take(count).ToArray();
                }
            }
            Offset += count;

            return buffer;
        }

        internal virtual void WriteBytes(byte[] buffer)
        {
            if (isRead) throw new Exception("This binary buffer should only be read");
            Buffer.InsertRange(Offset, buffer);
            Offset += buffer.Length;
        }

        internal virtual void WriteS8(sbyte value)
        {
            WriteBytes(new byte[] { (byte)value });
        }

        internal virtual void WriteS16(short value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual void WriteS32(int value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual void WriteS64(long value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual void WriteU8(byte value)
        {
            WriteBytes(new byte[] { value });
        }

        internal virtual void WriteU16(ushort value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual void WriteU32(uint value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual void WriteU64(ulong value)
        {
            WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        internal virtual sbyte ReadS8()
        {
            return (sbyte)ReadBytes(sizeof(byte))[0];
        }

        internal virtual short ReadS16()
        {
            return BitConverter.ToInt16(ReadBytes(sizeof(short)).Reverse().ToArray(), 0);
        }

        internal virtual int ReadS32()
        {
            return BitConverter.ToInt32(ReadBytes(sizeof(int)).Reverse().ToArray(), 0);
        }

        internal virtual long ReadS64()
        {
            return BitConverter.ToInt64(ReadBytes(sizeof(long)).Reverse().ToArray(), 0);
        }

        internal virtual byte ReadU8()
        {
            return ReadBytes(sizeof(byte))[0];
        }

        internal virtual ushort ReadU16()
        {
            return BitConverter.ToUInt16(ReadBytes(sizeof(short)).Reverse().ToArray(), 0);
        }

        internal virtual uint ReadU32()
        {
            return BitConverter.ToUInt32(ReadBytes(sizeof(int)).Reverse().ToArray(), 0);
        }

        internal virtual ulong ReadU64()
        {
            return BitConverter.ToUInt64(ReadBytes(sizeof(long)).Reverse().ToArray(), 0);
        }

        internal void Pad()
        {
            while (Buffer.Count % 4 != 0)
                Buffer.Add(0);
        }

        internal byte[] ToArray()
        {
            return Buffer.ToArray();
        }

        internal long Length
        {
            get
            {
                return Buffer.Count();
            }
        }

        internal byte this[int index]
        {
            get
            {
                return Buffer[index];
            }
        }
    }
}
