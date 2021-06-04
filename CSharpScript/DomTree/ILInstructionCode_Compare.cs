namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Compare : ILInstructionCode
	{
		public BaseCompare Compare;
		public ILInstructionCode_Compare(BaseCompare compare, int offset, int length)
			: base(offset, length, ILCodeFlag.Compare)
		{
			Compare = compare;
		}
		public override string Print()
		{
			return Compare.ToString();
		}
	}
}
