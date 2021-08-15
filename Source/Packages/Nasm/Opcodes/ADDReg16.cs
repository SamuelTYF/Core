using System;
using System.IO;

namespace Nasm.Opcodes
{
    public class ADDReg16 : Opcode
    {
        public Reg16 RS;
        public Reg16 RT;
        public ADDReg16(Reg16 rs,Reg16 rt):base(2)
        {
            RS = rs;
            RT = rt;
        }
        public override byte[] Get(Compiler compiler)
        {
            byte[] bs = new byte[2];
            bs[0] = 0x01;
            bs[1] = (byte)(0xc0 | ((byte)RT << 3) | ((byte)RS));
            return bs;
        }
    }
}
