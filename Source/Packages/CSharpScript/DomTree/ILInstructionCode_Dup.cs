namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Dup : ILInstructionCode
	{
		public ILInstructionCode_Dup(int offset, int length)
			: base(offset, length, ILCodeFlag.Dup)
		{
		}
		public override string Print()
		{
			return "dup";
		}
	}
}
