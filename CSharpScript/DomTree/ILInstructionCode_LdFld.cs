using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdFld : ILInstructionCode
	{
		public FieldOrRef Field;
		public ILInstructionCode_LdFld(FieldOrRef field, int offset, int length)
			: base(offset, length, ILCodeFlag.LdFld)
		{
			Field = field;
		}
		public override string Print()
		{
			return $"ldfld          {Field}";
		}
	}
}
