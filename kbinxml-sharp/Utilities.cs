using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal static class Utilities
    {
        internal static byte[] Slice(this byte[] array,
            int startIndex, int range)
        {
            int length = range - startIndex;
            byte[] output = new byte[length];
            Array.Copy(array, startIndex, output, 0, length);

            return output;
        }

        internal static int ToInt32(this byte[] array)
        {
            Array.Reverse(array); 
            int output = BitConverter.ToInt32(array, 0);

            return output;
        }
    }
}
