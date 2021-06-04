using System;
namespace CSharpScript.File
{
	public sealed class Instruction_ShortInlineR : Instruction
	{
		public float Value;
		public Instruction_ShortInlineR(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToSingle(bs, index + 1);
			index += 4;
		}
	}
}
