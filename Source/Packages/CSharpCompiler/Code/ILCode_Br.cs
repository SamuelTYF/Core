using CSharpCompiler.Metadata;

namespace CSharpCompiler.Code
{
    public enum Br_Operator
    {
		br,
		brfalse,
		brtrue,
		beq,
		bne,
		bge,
		bgt,
		ble,
		blt
	}
    public class ILCode_Br:ILCode
    {
		public Br_Operator Operator;
		public ILCode_Block Target;
		public ILCode_Br(ILCode_Block parent, Br_Operator @operator,ILCode_Block target) : base(parent)
        {
			Operator = @operator;
			Target = target;
        }
		public override int GetLength() => 2;
		public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\t{Operator}\tIL_{Convert.ToString(Target.Offset.Value,16).PadLeft(4,'0')}";
	}
}
