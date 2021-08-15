namespace CSharpScript.DomTree
{
	public class ILInstructionCode_RefAnyType : ILInstructionCode
	{
		public ILInstructionCode_RefAnyType(int offset, int length)
			: base(offset, length, ILCodeFlag.RefAnyType)
		{
		}
		public override string Print()
		{
			return "refanytype";
		}
	}
}
