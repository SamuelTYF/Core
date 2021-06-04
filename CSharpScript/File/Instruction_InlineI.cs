using System;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineI : Instruction
	{
		public int Value;
		public Instruction_InlineI(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}{Value}";
		}
	}
}
