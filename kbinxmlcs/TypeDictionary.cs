using System.Collections.Generic;
using System.Linq;

namespace kbinxmlcs
{
    internal static class TypeDictionary
    {
        internal static Dictionary<byte, Type> TypeMap = new Dictionary<byte, Type>()
        {
           { 2,  new KbinSByte()           },
           { 16, new KbinSByte("2s8", 2)   },
           { 26, new KbinSByte("3s8", 3)   },
           { 36, new KbinSByte("4s8", 4)   },
           { 48, new KbinSByte("vs8", 16)  },
           { 3,  new KbinByte()            },
           { 17, new KbinByte("2u8", 2)    },
           { 27, new KbinByte("3u8", 3)    },
           { 37, new KbinByte("4u8", 4)    },
           { 49, new KbinByte("vu8", 16)   },
           { 52, new KbinByte("bool")      },
           { 53, new KbinByte("2b", 2)     },
           { 54, new KbinByte("3b", 3)     },
           { 55, new KbinByte("4b", 4)     },
           { 56, new KbinByte("vb", 16)    },
           { 4,  new KbinShort()           },
           { 18, new KbinShort("2s16", 2)  },
           { 28, new KbinShort("3s16", 3)  },
           { 38, new KbinShort("4s16", 4)  },
           { 50, new KbinShort("vs16", 8)  },
           { 5,  new KbinUShort()          },
           { 19, new KbinUShort("2u16", 2) },
           { 29, new KbinUShort("3u16", 3) },
           { 39, new KbinUShort("4u16", 4) },
           { 51, new KbinUShort("vu16", 8) },
           { 6,  new KbinInt()             },
           { 20, new KbinInt("2s32", 2)    },
           { 30, new KbinInt("3s32", 3)    },
           { 40, new KbinInt("vs32", 4)    },
           { 13, new KbinInt("time")       },
           { 7,  new KbinUInt()            },
           { 21, new KbinUInt("2u32", 2)   },
           { 31, new KbinUInt("3u32", 3)   },
           { 41, new KbinUInt("vu32", 4)   },
           { 8,  new KbinLong()            },
           { 22, new KbinLong("vs64", 2)   },
           { 32, new KbinLong("3s64", 3)   },
           { 42, new KbinLong("4s64", 4)   },
           { 9,  new KbinULong()            },
           { 23, new KbinULong("vu64", 2)   },
           { 33, new KbinULong("3u64", 3)   },
           { 43, new KbinULong("4u64", 4)   },
           { 12, new KbinIpAddress()       },
           { 14, new KbinSingle()           },
           { 24, new KbinSingle("2f", 2)    },
           { 34, new KbinSingle("3f", 3)    },
           { 44, new KbinSingle("vf", 4)    },
           { 15, new KbinDouble()          },
           { 25, new KbinDouble("vd", 2)   },
           { 35, new KbinDouble("3d", 3)   },
           { 45, new KbinDouble("4d", 4)   },
           { 10, new KbinBinary()          },
           { 11, new KbinString()          },
        };

        internal static Dictionary<string, byte> ReverseTypeMap = TypeMap.ToDictionary(x => x.Value.Alias, x => x.Key);
    }
}
