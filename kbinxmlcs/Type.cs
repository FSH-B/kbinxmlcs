using System;
using System.Linq;
using System.Net;

namespace kbinxmlcs
{
    internal abstract class Type
    {
        internal int Size { get; }

        internal int Count { get; }

        internal string Alias { get; }

        internal Type(string alias, int size, int count = 1)
        {
            Alias = alias;
            Size = size;
            Count = count;
        }

        internal abstract string ToString(byte[] buffer);

        internal abstract byte[] GetBytes(string input);
    }

    internal class KbinByte : Type
    {
        public KbinByte() :
            base("u8", 1, 1)
        {

        }

        public KbinByte(string alias, int count = 1) :
            base(alias, 1, count)
        {

        }

        internal override byte[] GetBytes(string input) => new byte[] { byte.Parse(input) };

        internal override string ToString(byte[] buffer) => buffer[0].ToString();
    }

    internal class KbinSByte : Type
    {
        public KbinSByte() :
            base("s8", 1, 1)
        {

        }

        public KbinSByte(string alias, int count = 1) :
            base(alias, 1, count)
        {

        }

        internal override byte[] GetBytes(string input) => new byte[] { (byte)sbyte.Parse(input) };

        internal override string ToString(byte[] buffer) => ((sbyte)buffer[0]).ToString();
    }

    internal class KbinUShort : Type
    {
        public KbinUShort() :
            base("u16", 2, 1)
        {

        }

        public KbinUShort(string alias, int count = 1) :
            base(alias, 2, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(ushort.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToUInt16(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinShort : Type
    {
        public KbinShort() :
            base("s16", 2, 1)
        {

        }

        public KbinShort(string alias, int count = 1) :
            base(alias, 2, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(short.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToInt16(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinUInt : Type
    {
        public KbinUInt() :
            base("u32", 4, 1)
        {

        }

        public KbinUInt(string alias, int count = 1) :
            base(alias, 4, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(uint.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToUInt32(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinInt : Type
    {
        public KbinInt() :
            base("s32", 4, 1)
        {

        }

        public KbinInt(string alias, int count = 1) :
            base(alias, 4, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(int.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToInt32(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinULong : Type
    {
        public KbinULong() :
            base("u64", 8, 1)
        {

        }

        public KbinULong(string alias, int count = 1) :
            base(alias, 8, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(ulong.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToUInt64(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinLong : Type
    {
        public KbinLong() :
            base("s64", 8, 1)
        {

        }

        public KbinLong(string alias, int count = 1) :
            base(alias, 8, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(long.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToInt64(buffer.Reverse().ToArray()).ToString();
    }

    internal class KbinIpAddress : Type
    {
        public KbinIpAddress() :
            base("ip4", 4, 1)
        {

        }

        public KbinIpAddress(string alias, int count = 1) :
            base(alias, 4, count)
        {

        }

        internal override byte[] GetBytes(string input) => IPAddress.Parse(input).GetAddressBytes();

        internal override string ToString(byte[] buffer) => new IPAddress(buffer).ToString();
    }

    internal class KbinSingle : Type
    {
        public KbinSingle() :
            base("float", 4, 1)
        {

        }

        public KbinSingle(string alias, int count = 1) :
            base(alias, 4, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(float.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToSingle(buffer.Reverse().ToArray()).ToString("0.000000");
    }

    internal class KbinDouble : Type
    {
        public KbinDouble() :
            base("double", 8, 1)
        {

        }

        public KbinDouble(string alias, int count = 1) :
            base(alias, 8, count)
        {

        }

        internal override byte[] GetBytes(string input) => BitConverter.GetBytes(double.Parse(input)).Reverse().ToArray();

        internal override string ToString(byte[] buffer) => BitConverter.ToDouble(buffer.Reverse().ToArray()).ToString("0.000000");
    }

    internal class KbinString : Type
    {
        public KbinString() :
            base("str", -1, -1)
        {

        }

        public KbinString(string alias, int count = 1) :
            base(alias, -1, count)
        {

        }

        internal override byte[] GetBytes(string input) => throw new NotSupportedException();

        internal override string ToString(byte[] buffer) => throw new NotSupportedException();
    }

    internal class KbinBinary : Type
    {
        public KbinBinary() :
            base("bin", -1, -1)
        {

        }

        public KbinBinary(string alias, int count = 1) :
            base(alias, -1, count)
        {

        }

        internal override byte[] GetBytes(string input) => throw new NotSupportedException();

        internal override string ToString(byte[] buffer) => throw new NotSupportedException();
    }
}
