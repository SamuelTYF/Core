namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Volatile : ILInstructionCode
	{
		public ILInstructionCode_Volatile(int offset, int length)
			: base(offset, length, ILCodeFlag.Volatile)
		{
		}
		public override string Print()
		{
			return "volatile";
		}
	}
}
