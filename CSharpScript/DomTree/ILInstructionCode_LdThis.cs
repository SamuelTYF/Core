using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdThis : ILInstructionCode
	{
		public TypeDef Type;
		public ILInstructionCode_LdThis(TypeDef type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdThis)
		{
			Type = type;
		}
		public override string Print()
		{
			return "ldarg          this";
		}
	}
}
