using CSharpCompiler.Metadata;

namespace CSharpCompiler.Code
{
    public class ILCode_StLoc : ILCode
    {
        public Variable Variable;
        public ILCode_StLoc(ILCode_Block parent, Variable variable) : base(parent)
            => Variable = variable;
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value,16).PadLeft(4,'0')}:\tstloc\t{Variable.Name}";
    }
}
