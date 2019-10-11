## kbinxmlcs

A tool for decoding Konami's binary XML format.

## Usage in C#:

```cs
using System;
using System.IO;
using kbinxmlcs;

public class Program
{
    static void Main(string[] args)
    {
        byte[] data = File.ReadAllBytes("test.bin");
        XmlReader XmlReader = new XmlReader(data);
        Console.WriteLine(XmlReader.Read().OuterXml);
    }
}
```
