using Collection;
using System;
using System.IO;
using static System.Console;
namespace JPEG
{
    class Program
    {
        static void Main()
        {
            using Stream stream = new FileInfo("2.jpg").OpenRead();
            List<Section> sections = new();
            while (stream.Position<stream.Length)
                sections.Add(Section.GetSection(stream));
        }
    }
}
