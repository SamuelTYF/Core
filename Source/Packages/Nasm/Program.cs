using Nasm.Opcodes;
using Parser;
using System;
using System.IO;

namespace Nasm
{
    class Program
    {
        static void Main()
        {
            Compiler compiler = new();
            byte[] bin=compiler.Run(0,
                new MOV16(Reg16.AX, 12),
                new Label("start"),
                new ADDReg16(Reg16.AX,Reg16.BX),
                new JMP("start")
                );
            using Stream output = new FileInfo(@"D:\OS\Test\bin\Debug\netcoreapp2.0\cosmos\2.bin").OpenWrite();
            output.Write(bin, 0, bin.Length);
        }
    }
}
