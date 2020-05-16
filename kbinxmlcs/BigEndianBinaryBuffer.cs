using System;
using System.Collections.Generic;
using System.Linq;

namespace kbinxmlcs
{
    internal class BigEndianBinaryBuffer
    {
        protected List<byte> Buffer;
        protected int Offset;

        internal BigEndianBinaryBuffer(byte[] buffer) => Buffer = new List<byte>(buffer);

        internal BigEndianBinaryBuffer() => Buffer = new List<byte>();

        internal virtual byte[] ReadBytes(int count)
        {
            var buffer = new byte[count];
            Buffer.CopyTo(Offset, buffer, 0, count);
            Offset += count;

            return buffer;
        }

        internal virtual void WriteBytes(byte[] buffer)
        {
            Buffer.InsertRange(Offset, buffer);
            Offset += buffer.Length;
        }

        internal virtual void WriteS8(sbyte value) => WriteBytes(new byte[] { (byte)value });

        internal virtual void WriteS16(short value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual void WriteS32(int value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual void WriteS64(long value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual void WriteU8(byte value) => WriteBytes(new byte[] { value });

        internal virtual void WriteU16(ushort value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual void WriteU32(uint value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual void WriteU64(ulong value) => WriteBytes(BitConverter.GetBytes(value).Reverse().ToArray());

        internal virtual sbyte ReadS8() => (sbyte)ReadBytes(sizeof(byte))[0];

        internal virtual short ReadS16() => BitConverter.ToInt16(ReadBytes(sizeof(short)).Reverse().ToArray(), 0);

        internal virtual int ReadS32() => BitConverter.ToInt32(ReadBytes(sizeof(int)).Reverse().ToArray(), 0);

        internal virtual long ReadS64() => BitConverter.ToInt64(ReadBytes(sizeof(long)).Reverse().ToArray(), 0);

        internal virtual byte ReadU8() => ReadBytes(sizeof(byte))[0];

        internal virtual ushort ReadU16() => BitConverter.ToUInt16(ReadBytes(sizeof(short)).Reverse().ToArray(), 0);

        internal virtual uint ReadU32() => BitConverter.ToUInt32(ReadBytes(sizeof(int)).Reverse().ToArray(), 0);

        internal virtual ulong ReadU64() => BitConverter.ToUInt64(ReadBytes(sizeof(long)).Reverse().ToArray(), 0);

        internal void Pad()
        {
            while (Buffer.Count % 4 != 0)
                Buffer.Add(0);
        }

        internal byte[] ToArray() => Buffer.ToArray();

        internal int Length => Buffer.Count();

        internal byte this[int index] => Buffer[index];
    }
}
