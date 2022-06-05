namespace CSharpCompiler.Code
{
    public class ILCode_Ret : ILCode
    {
        public ILCode_Ret(ILCode_Block parent) : base(parent) { }
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\tret";
    }
}
