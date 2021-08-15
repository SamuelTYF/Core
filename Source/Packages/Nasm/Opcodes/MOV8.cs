using System.IO;

namespace Nasm.Opcodes
{
    public class MOV8 : Opcode
    {
        public Reg8 RS;
        public byte Imm;
        public MOV8(Reg8 rs, byte imm):base(2)
        {
            RS = rs;
            Imm = imm;
        }
        public override byte[] Get(Compiler compiler)
        {
            byte[] bs = new byte[2];
            bs[0] = (byte)(0xB0 | ((byte)RS));
            bs[1] = Imm;
            return bs;
        }
    }
}