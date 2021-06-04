using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdFldA : ILInstructionCode
	{
		public FieldOrRef Field;
		public ILInstructionCode_LdFldA(FieldOrRef field, int offset, int length)
			: base(offset, length, ILCodeFlag.LdFldA)
		{
			Field = field;
		}
		public override string Print()
		{
			return $"ldflda         {Field}";
		}
	}
}
