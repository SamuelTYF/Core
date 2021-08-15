namespace CSharpScript.DomTree
{
	public class ILInstructionCode_EndFinally : ILInstructionCode
	{
		public ILInstructionCode_EndFinally(int offset, int length)
			: base(offset, length, ILCodeFlag.EndFinally)
		{
		}
		public override string Print()
		{
			return "endfinally";
		}
	}
}
