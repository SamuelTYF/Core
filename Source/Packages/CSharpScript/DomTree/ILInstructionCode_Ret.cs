namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Ret : ILInstructionCode
	{
		public ILInstructionCode_Ret(int offset, int length)
			: base(offset, length, ILCodeFlag.Ret)
		{
		}
		public override string Print()
		{
			return "ret";
		}
	}
}
