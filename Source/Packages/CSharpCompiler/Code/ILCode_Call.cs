using CSharpCompiler.Metadata;

namespace CSharpCompiler.Code
{
    public class ILCode_Call : ILCode
    {
        public IMethod Method;
        public ILCode_Call(ILCode_Block parent, IMethod method) : base(parent)
            => Method = method;
        public override int GetLength() => 5;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\tcall\t{Method.BaseString()}";
    }
}
