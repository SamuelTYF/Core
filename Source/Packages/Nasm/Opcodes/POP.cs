namespace Nasm.Opcodes
{
    public class POP : Opcode
    {
        public Reg16 RS;
        public short Imm;
        public POP(Reg16 rs, short imm):base(1)
        {
            RS = rs;
            Imm = imm;
        }
        public override byte[] Get(Compiler compiler)
        {
            byte[] bs = new byte[1];
            bs[0] = (byte)(0x58 | ((byte)RS));
            return bs;
        }
    }
}
