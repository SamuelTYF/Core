using System;
using System.IO;

namespace ELF
{
    class Program
    {
        static void Main()
        {
            //using Stream stream = new FileInfo(@"D:\OS\Test\bin\Debug\netcoreapp2.0\cosmos\Test.bin").OpenRead();
            using Stream stream = new FileInfo(@"D:\OS\Test\bin\Debug\netcoreapp2.0\cosmos\1.obj").OpenRead();
            new ELF(stream);
        }
    }
}
