namespace CSharpScript.File
{
	public sealed class Instruction_InlineNone : Instruction
	{
		public Instruction_InlineNone(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 0, op)
		{
		}
		public override string ToString()
		{
			return GetPrefix();
		}
	}
}
