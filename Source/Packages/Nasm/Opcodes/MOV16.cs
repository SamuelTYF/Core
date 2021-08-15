using System;
using System.IO;

namespace Nasm.Opcodes
{
    public class MOV16 : Opcode
    {
        public Reg16 RS;
        public short Imm;
        public MOV16(Reg16 rs,short imm):base(3)
        {
            RS = rs;
            Imm = imm;
        }
        public override byte[] Get(Compiler compiler)
        {
            byte[] bs = new byte[3];
            bs[0] = (byte)(0xB8 | ((byte)RS));
            bs[1] = (byte)(Imm & 0xF);
            bs[2]= (byte)(Imm >> 8);
            return bs;
        }
    }
}
