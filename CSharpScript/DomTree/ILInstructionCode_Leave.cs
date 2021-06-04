namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Leave : ILInstructionCode
	{
		public int TargetIndex;
		public ILInstructionCode_Leave(int target, int offset, int length)
			: base(offset, length, ILCodeFlag.Leave)
		{
			TargetIndex = target;
		}
		public override string Print()
		{
			return $"leave          {TargetIndex}";
		}
	}
}
