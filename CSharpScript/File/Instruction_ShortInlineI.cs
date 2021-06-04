namespace CSharpScript.File
{
	public sealed class Instruction_ShortInlineI : Instruction
	{
		public byte Value;
		public Instruction_ShortInlineI(int ilIndex, Operator op, byte[] bs, ref int index)
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
