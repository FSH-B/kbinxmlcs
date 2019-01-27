using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal static class Sixbit
    {
        static string Map(this int[] charBytes)
        {
            string[] result = new string[charBytes.Length];
            for (int i = 0; i < charBytes.Length; i++)
            {
                result[i] = charmap[charBytes[i]].ToString();
            }
            return string.Join("", result);
        }

        public static string Unpack(this byte[] input, int length)
        {
            int[] charBytes = new int[length];
            for (int i = 0; i < length * 6; i++)
            {
                charBytes[i / 6] |= (input[i / 8] >> 7 - (i % 8) & 1) << (5 - (i % 6));
            }
            return charBytes.Map();
        }

        private static string charmap = "0123456789:ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";
    }
}
