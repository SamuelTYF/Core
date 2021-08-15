namespace Nasm.Opcodes
{
    public abstract class Opcode
    {
        public long Address;
        public int Length;
        public Opcode(int length) => Length = length;
        public abstract byte[] Get(Compiler compiler);
    }
}
