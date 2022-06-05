using CSharpCompiler.Metadata;

namespace CSharpCompiler.Code
{
    public class ILCode_LdLoc : ILCode
    {
        public Variable Variable;
        public ILCode_LdLoc(ILCode_Block parent, Variable variable) : base(parent)
            => Variable = variable;
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value,16).PadLeft(4,'0')}:\tldloc\t{Variable.Name}";
    }
}
