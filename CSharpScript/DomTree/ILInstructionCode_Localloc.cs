namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Localloc : ILInstructionCode
	{
		public ILInstructionCode_Localloc(int offset, int length)
			: base(offset, length, ILCodeFlag.Localloc)
		{
		}
		public override string Print()
		{
			return "localloc";
		}
	}
}
