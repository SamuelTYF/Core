namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Br : ILInstructionCode
	{
		public Condition Condition;
		public int TargetIndex;
		public ILInstructionCode_Br(Condition condition, int target, int offset, int length)
			: base(offset, length, ILCodeFlag.Br)
		{
			Condition = condition;
			TargetIndex = target;
		}
		public override string Print()
		{
			return string.Format("{0,-15}{1}", Condition, TargetIndex);
		}
	}
}
