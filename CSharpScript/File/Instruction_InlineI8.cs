using System;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineI8 : Instruction
	{
		public long Value;
		public Instruction_InlineI8(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 8, op)
		{
			Value = BitConverter.ToInt64(bs, index + 1);
			index += 8;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}{Value}";
		}
	}
}
