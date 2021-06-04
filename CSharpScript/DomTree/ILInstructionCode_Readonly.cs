namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Readonly : ILInstructionCode
	{
		public ILInstructionCode_Readonly(int offset, int length)
			: base(offset, length, ILCodeFlag.Readonly)
		{
		}
		public override string Print()
		{
			return "readonly";
		}
	}
}
