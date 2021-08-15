namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Nop : ILInstructionCode
	{
		public ILInstructionCode_Nop(int offset, int length)
			: base(offset, length, ILCodeFlag.Nop)
		{
		}
        public override string Print() => "nop";
    }
}
