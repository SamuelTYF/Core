using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdFieldToken : ILInstructionCode_LdToken
	{
		public Field Field;
		public ILInstructionCode_LdFieldToken(Field field, int offset, int length)
			: base(offset, length, ILCodeFlag.LdFieldToken)
		{
			Field = field;
		}
		public override string Print()
		{
			return $"ldtoken        {Field}";
		}
	}
}
