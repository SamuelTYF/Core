namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Pop : ILInstructionCode
	{
		public ILInstructionCode_Pop(int offset, int length)
			: base(offset, length, ILCodeFlag.Pop)
		{
		}
		public override string Print()
		{
			return "pop";
		}
	}
}
