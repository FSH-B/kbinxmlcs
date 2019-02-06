using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal class Encodings
    {
        public static string[] encodings = 
            { "SHIFT_JIS", "ASCII", "ISO-8859-1",
              "EUC-JP", "SHIFT_JIS", "UTF-8" };
    }

    public struct ConstructTypes
    {
        public string name;
        public int count, size;
    }

    public enum ControlTypes
    {
        NodeStart,
        Attribute,
        NodeEnd,
        FileEnd        
    }

    public class Types
    {
        public static Dictionary<int, ControlTypes> ControlTypeMap =
            new Dictionary<int, ControlTypes>()
        {
            { 1, ControlTypes.NodeStart  },
            { 46, ControlTypes.Attribute },
            { 190, ControlTypes.NodeEnd  },
            { 191, ControlTypes.FileEnd  }
        };

        public static Dictionary<int, ConstructTypes> ConstructTypeMap =
            new Dictionary<int, ConstructTypes>()
        {
            { 2, new ConstructTypes { name="s8", size=1, count=1 }   },
            { 3, new ConstructTypes { name="u8", size=1, count=1 }   },
            { 4, new ConstructTypes { name="s16", size=2, count=1}   },
            { 5, new ConstructTypes { name="u16", size=2, count=1}   },
            { 6, new ConstructTypes { name="s32", size=4, count=1}   },
            { 7, new ConstructTypes { name="u32", size=4, count=1}   },
            { 8, new ConstructTypes { name="s64", size=8, count=1}   },
            { 9, new ConstructTypes { name="u64", size=8, count=1}   },
            { 10, new ConstructTypes { name="bin", size=-1, count=1}  },
            { 11, new ConstructTypes { name="str", size=-1, count=1}  },
            { 12, new ConstructTypes { name="ip4", size=4, count=1}  }, 
            { 13, new ConstructTypes { name="time", size=4, count=1} },
            { 37, new ConstructTypes { name="4u8", size=1, count=4}  },
            { 52, new ConstructTypes { name="bool", size=1, count=1} },
        };
    }
}

