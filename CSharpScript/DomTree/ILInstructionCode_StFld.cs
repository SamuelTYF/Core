using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StFld : ILInstructionCode
	{
		public FieldOrRef Field;
		public ILInstructionCode_StFld(FieldOrRef field, int offset, int length)
			: base(offset, length, ILCodeFlag.StFld)
		{
			Field = field;
		}
		public override string Print()
		{
			return $"stfld          {Field}";
		}
	}
}
