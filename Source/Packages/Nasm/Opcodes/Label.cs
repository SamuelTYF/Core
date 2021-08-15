using System;
namespace Nasm.Opcodes
{
    public class Label:Opcode
    {
        public string Name;
        public Label(string name) : base(0) => Name = name;
        public override byte[] Get(Compiler compiler)
        {
            compiler.Labels[Name] = Address;
            return Array.Empty<byte>();
        }
    }
}
