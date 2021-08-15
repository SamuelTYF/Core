using System;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineR : Instruction
	{
		public double Value;
		public Instruction_InlineR(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 8, op)
		{
			Value = BitConverter.ToDouble(bs, index + 1);
			index += 8;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}{Value}";
		}
	}
}
