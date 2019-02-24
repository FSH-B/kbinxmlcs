using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kbinxml_sharp
{
    internal static class Encodings
    {
        public static string[] encodings = 
            { "SHIFT_JIS", "ASCII", "ISO-8859-1",
              "EUC-JP", "SHIFT_JIS", "UTF-8" };
    }

    internal struct ConstructTypes
    {
        public string name, type;
        public int count, size;
    }

    internal enum ControlTypes
    {
        NodeStart,
        Attribute,
        NodeEnd,
        FileEnd        
    }

    internal static class Types
    {
        internal static Dictionary<int, ControlTypes> ControlTypeMap =
            new Dictionary<int, ControlTypes>()
        {
            { 1, ControlTypes.NodeStart  },
            { 46, ControlTypes.Attribute },
            { 190, ControlTypes.NodeEnd  },
            { 191, ControlTypes.FileEnd  }
        };

        internal static Dictionary<int, ConstructTypes> ConstructTypeMap =
            new Dictionary<int, ConstructTypes>()
        {
            { 2, new ConstructTypes { name="s8", size=1, count=1, type="s8"       } },
            { 3, new ConstructTypes { name="u8", size=1, count=1, type="u8"       } },
            { 4, new ConstructTypes { name="s16", size=2, count=1, type="s16"     } },
            { 5, new ConstructTypes { name="u16", size=2, count=1, type="u16"     } },
            { 6, new ConstructTypes { name="s32", size=4, count=1, type="s32"     } },
            { 7, new ConstructTypes { name="u32", size=4, count=1, type="u32"     } },
            { 8, new ConstructTypes { name="s64", size=8, count=1, type="s64"     } },
            { 9, new ConstructTypes { name="u64", size=8, count=1, type="u64"     } },
            { 10, new ConstructTypes { name="bin", size=-1, count=1, type=""      } },
            { 11, new ConstructTypes { name="str", size=-1, count=1, type=""      } },
            { 12, new ConstructTypes { name="ip4", size=4, count=1, type="ip4"    } },
            { 13, new ConstructTypes { name="time", size=4, count=1, type="u32"   } },
            { 14, new ConstructTypes { name="float", size=4, count=1, type="f32"  } },
            { 15, new ConstructTypes { name="double", size=8, count=1, type="f64" } },
            { 16, new ConstructTypes { name="2s8", size=1, count=2, type="s8"     } },
            { 17, new ConstructTypes { name="2u8", size=1, count=2, type="u8"     } },
            { 18, new ConstructTypes { name="2s16", size=2, count=2, type="s16"   } },
            { 19, new ConstructTypes { name="2u16", size=2, count=2, type="u16"   } },
            { 20, new ConstructTypes { name="2s32", size=4, count=2, type="s32"   } },
            { 21, new ConstructTypes { name="2u32", size=4, count=2, type="u32"   } },
            { 22, new ConstructTypes { name="vs64", size=8, count=2, type="s64"   } },
            { 23, new ConstructTypes { name="vu64", size=8, count=2, type="u64"   } },
            { 24, new ConstructTypes { name="2f", size=4, count=2, type="f32"     } },
            { 25, new ConstructTypes { name="vd", size=8, count=2, type="f64"     } },
            { 26, new ConstructTypes { name="3s8", size=1, count=3, type="s8"     } },
            { 27, new ConstructTypes { name="3u8", size=1, count=3, type="u8"     } },
            { 28, new ConstructTypes { name="3s16", size=2, count=3, type="s16"   } },
            { 29, new ConstructTypes { name="3u16", size=2, count=3, type="u16"   } },
            { 30, new ConstructTypes { name="3s32", size=4, count=3, type="s32"   } },
            { 31, new ConstructTypes { name="3u32", size=4, count=3, type="u32"   } },
            { 32, new ConstructTypes { name="3s64", size=8, count=3, type="s64"   } },
            { 33, new ConstructTypes { name="3u64", size=8, count=3, type="u64"   } },
            { 34, new ConstructTypes { name="3f", size=4, count=3, type="f32"     } },
            { 35, new ConstructTypes { name="3d", size=8, count=3, type="f64"     } },
            { 36, new ConstructTypes { name="4s8", size=1, count=4, type="s8"     } },
            { 37, new ConstructTypes { name="4u8", size=1, count=4, type="u8"     } },
            { 38, new ConstructTypes { name="4s16", size=2, count=4, type="s16"   } },
            { 39, new ConstructTypes { name="4u16", size=2, count=4, type="u16"   } },
            { 40, new ConstructTypes { name="vs32", size=4, count=4, type="s32"   } },
            { 41, new ConstructTypes { name="vu32", size=4, count=4, type="u32"   } },
            { 42, new ConstructTypes { name="4s64", size=8, count=4, type="s64"   } },
            { 43, new ConstructTypes { name="4u64", size=8, count=4, type="u64"   } },
            { 44, new ConstructTypes { name="vf", size=4, count=4, type="f32"     } },
            { 45, new ConstructTypes { name="4d", size=8, count=4, type="f64"     } },
            { 48, new ConstructTypes { name="vs8", size=1, count=16, type="s8"    } },
            { 49, new ConstructTypes { name="vu8", size=1, count=16, type="u8"    } },
            { 50, new ConstructTypes { name="vs16", size=2, count=8, type="s16"   } },
            { 51, new ConstructTypes { name="vu16", size=2, count=8, type="u16"   } },
            { 52, new ConstructTypes { name="bool", size=1, count=1, type="u8"    } },
            { 53, new ConstructTypes { name="2b", size=1, count=2, type="u8"      } },
            { 54, new ConstructTypes { name="3b", size=1, count=3, type="u8"      } },
            { 55, new ConstructTypes { name="4b", size=1, count=4, type="u8"      } },
            { 56, new ConstructTypes { name="vb", size=1, count=16, type="u8"     } },
        };
    }
}

