using System;
using System.Linq;
using System.Net;

namespace kbinxmlcs
{
    public static class Converters
    {
        public delegate byte[] StringConverter(string str);

        public static byte[] U8ToBytes(string str) => new[] { byte.Parse(str) };
        public static byte[] S8ToBytes(string str) => new[] { (byte)sbyte.Parse(str) };
        public static byte[] U16ToBytes(string str) => BitConverter.GetBytes(ushort.Parse(str)).Reverse().ToArray();
        public static byte[] S16ToBytes(string str) => BitConverter.GetBytes(short.Parse(str)).Reverse().ToArray();
        public static byte[] U32ToBytes(string str) => BitConverter.GetBytes(uint.Parse(str)).Reverse().ToArray();
        public static byte[] S32ToBytes(string str) => BitConverter.GetBytes(int.Parse(str)).Reverse().ToArray();
        public static byte[] U64ToBytes(string str) => BitConverter.GetBytes(ulong.Parse(str)).Reverse().ToArray();
        public static byte[] S64ToBytes(string str) => BitConverter.GetBytes(long.Parse(str)).Reverse().ToArray();
        public static byte[] Ip4ToBytes(string input) => IPAddress.Parse(input).GetAddressBytes();
        public static byte[] SingleToBytes(string input) => BitConverter.GetBytes(float.Parse(input)).Reverse().ToArray();
        public static byte[] DoubleToBytes(string input) => BitConverter.GetBytes(double.Parse(input)).Reverse().ToArray();
        
        public delegate string ByteConverter(byte[] bytes);

        public static string U8ToString(byte[] bytes) => bytes[0].ToString();
        public static string S8ToString(byte[] bytes) => ((sbyte)bytes[0]).ToString();
        public static string U16ToString(byte[] bytes) => BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0).ToString();
        public static string S16ToString(byte[] bytes) => BitConverter.ToInt16(bytes.Reverse().ToArray(), 0).ToString();
        public static string U32ToString(byte[] bytes) => BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0).ToString();
        public static string S32ToString(byte[] bytes) => BitConverter.ToInt32(bytes.Reverse().ToArray(), 0).ToString();
        public static string U64ToString(byte[] bytes) => BitConverter.ToUInt64(bytes.Reverse().ToArray(), 0).ToString();
        public static string S64ToString(byte[] bytes) => BitConverter.ToInt64(bytes.Reverse().ToArray(), 0).ToString();
        public static string Ip4ToString(byte[] buffer) => new IPAddress(buffer).ToString();
        public static string SingleToString(byte[] buffer) => BitConverter.ToSingle(buffer.Reverse().ToArray(), 0).ToString("0.000000");
        public static string DoubleToString(byte[] buffer) => BitConverter.ToDouble(buffer.Reverse().ToArray(), 0).ToString("0.000000");
    }
}