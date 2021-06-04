namespace CSharpScript.DomTree
{
	public class ILInstructionCode_ArgList : ILInstructionCode
	{
		public ILInstructionCode_ArgList(int offset, int length)
			: base(offset, length, ILCodeFlag.ArgList)
		{
		}
		public override string Print()
		{
			return "arglist";
		}
	}
}
