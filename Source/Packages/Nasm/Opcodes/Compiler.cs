using Collection;
using System;

namespace Nasm.Opcodes
{
    public class Compiler
    {
        public AVL<string,long> Labels;
        public long Address;
        public long RunningAddress;
        public List<Opcode> RebuildCodes;
        public byte[] Run(long address,params Opcode[] codes)
        {
            Labels = new();
            Address = RunningAddress = address;
            RebuildCodes = new();
            List<byte> data = new();
            foreach(Opcode code in codes)
            {
                code.Address = RunningAddress;
                data.AddRange(code.Get(this));
                RunningAddress += code.Length;
            }
            byte[] bin = data.ToArray();
            foreach(Opcode code in RebuildCodes)
            {
                byte[] bs = code.Get(this);
                Array.Copy(bs, 0, bin, code.Address - Address, bs.Length);
            }
            return bin;
        }
    }
}
