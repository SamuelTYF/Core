namespace CSharpCompiler.Code
{
    public class ILCode_Block: ILCode
    {
        public List<ILCode> Codes;
        public ILCode_Block(ILCode_Block parent) : base(parent)
        {
            Codes = new();
        }
        public override int GetLength()=>Codes.Select(code => code.GetLength()).Sum();
        public override string ToString() => string.Join("\n", Codes.Select(code=>code.ToString()).Where(code=>code.Length>0));
        public void Update(int offset=0)
        {
            Offset = offset;
            int t = offset;
            foreach(ILCode code in Codes)
                if(code is ILCode_Block block)
                {
                    block.Update(t);
                    t += block.GetLength();
                }
                else
                {
                    code.Offset = t;
                    t += code.GetLength();
                }
        }
    }
}
