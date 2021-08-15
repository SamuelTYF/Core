namespace Nasm.Opcodes
{
    public class JMP:Opcode
    {
        public string Name;
        public JMP(string name) : base(2) => Name = name;
        public override byte[] Get(Compiler compiler)
        {
            if(compiler.Labels.ContainsKey(Name))
            {
                long offset = compiler.Labels[Name] - (Address + 2);
                byte[] bs = new byte[2];
                bs[0] = 0xEB;
                bs[1] = (byte)offset;
                return bs;
            }
            else
            {
                compiler.RebuildCodes.Add(this);
                return new byte[2];
            }
        }
    }
}
