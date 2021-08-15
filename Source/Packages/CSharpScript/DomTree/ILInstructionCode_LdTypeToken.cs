using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdTypeToken : ILInstructionCode_LdToken
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_LdTypeToken(TypeDefOrRef type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdTypeToken)
		{
			Type = type;
		}
		public override string Print()
		{
			return $"ldtoken        {Type}";
		}
	}
}
