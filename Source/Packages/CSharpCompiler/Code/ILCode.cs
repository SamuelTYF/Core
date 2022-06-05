namespace CSharpCompiler.Code
{
    public abstract class ILCode
    {
        public int? Offset;
        public ILCode_Block Parent;
        public ILCode(ILCode_Block parent)
        {
            Parent = parent;
            if (parent != null)
                parent.Codes.Add(this);
        }
        public abstract int GetLength();
    }
}
