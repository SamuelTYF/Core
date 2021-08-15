namespace CSharpScript.File
{
	public sealed class Instruction_ShortInlineVar : Instruction
	{
		public byte Value;
		public Instruction_ShortInlineVar(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 1, op)
		{
			Value = bs[index + 1];
			index++;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}{Value}";
		}
	}
}
