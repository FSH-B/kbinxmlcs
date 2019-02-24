using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace kbinxml_sharp
{
    internal class ByteConv
    {
        internal ByteConv(ConstructTypes constructTypes)
        {
            this.constructTypes = constructTypes;
        }

        internal string ConvertBytes(byte[] data)
        {
            if (data.Length != 0)
            {
                switch (constructTypes.type)
                {
                    case "s8":
                        return ConvertS8(data);
                    case "u8":
                        return ConvertU8(data);
                    case "s16":
                        return ConvertS16(data);
                    case "u16":
                        return ConvertU16(data);
                    case "s32":
                        return ConvertS32(data);
                    case "u32":
                        return ConvertU32(data);
                    case "s64":
                        return ConvertS64(data);
                    case "u64":
                        return ConvertU64(data);
                    case "ip4":
                        return ConvertIP4(data);
                    case "f32":
                        return ConvertFloat(data);
                    case "f64":
                        return ConvertDouble(data);
                    default:
                        return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private string ConvertS8(byte[] data)
        {
            return Array.ConvertAll(data, x => unchecked((sbyte)x))[0].ToString();
        }

        private string ConvertU8(byte[] data)
        {
            return data[0].ToString();
        }

        private string ConvertS16(byte[] data)
        {
            return BitConverter.ToInt16(data, 0).ToString();
        }

        private string ConvertU16(byte[] data)
        {
            return BitConverter.ToUInt16(data, 0).ToString();
        }

        private string ConvertS32(byte[] data)
        {
            return BitConverter.ToInt32(data, 0).ToString();
        }

        private string ConvertU32(byte[] data)
        {
            return BitConverter.ToUInt32(data, 0).ToString();
        }

        private string ConvertS64(byte[] data)
        {
            return BitConverter.ToInt64(data, 0).ToString();
        }

        private string ConvertU64(byte[] data)
        {
            return BitConverter.ToUInt64(data, 0).ToString();
        }

        private string ConvertIP4(byte[] data)
        {
            Array.Reverse(data);
            return new IPAddress(data).ToString();
        }

        private string ConvertFloat(byte[] data)
        {
            return BitConverter.ToSingle(data, 0).ToString("F6");
        }

        private string ConvertDouble(byte[] data)
        {
            return BitConverter.ToDouble(data, 0).ToString("F6");
        }


        private ConstructTypes constructTypes;
    }
}
