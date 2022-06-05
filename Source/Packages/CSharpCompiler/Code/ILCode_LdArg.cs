using CSharpCompiler.Metadata;

namespace CSharpCompiler.Code
{
    public class ILCode_LdArg : ILCode
    {
        public Parameter Parameter;
        public ILCode_LdArg(ILCode_Block parent, Parameter parameter) : base(parent)
            => Parameter = parameter;
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value,16).PadLeft(4,'0')}:\tldarg\t{Parameter.Name}";
    }
}
