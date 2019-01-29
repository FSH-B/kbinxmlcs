## kbinxml-sharp

A tool for decoding Konami's binary XML format.

## Usage in C#:

```cs
using System;
using System.IO;
using kbinxml_sharp;

public class Program
{
    static void Main(string[] args)
    {
        byte[] data = File.ReadAllBytes("test.bin");
        KbinReader kbinReader = new KbinReader(data);
        Console.WriteLine(kbinReader.XmlFromBinary().OuterXml);
    }
}
```

## Implementations in other programming languages

[kbinxml](https://github.com/mon/kbinxml) in Python, by [mon](https://github.com/mon)
[kbinxml-rs](https://github.com/mbilker/kbinxml-rs) in Rust, by [mbilker](https://github.com/mbilker)