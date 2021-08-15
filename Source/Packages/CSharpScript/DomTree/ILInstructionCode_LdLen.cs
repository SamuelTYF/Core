namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdLen : ILInstructionCode
	{
		public ILInstructionCode_LdLen(int offset, int length)
			: base(offset, length, ILCodeFlag.LdLen)
		{
		}
		public override string Print()
		{
			return "ldlen";
		}
	}
}
