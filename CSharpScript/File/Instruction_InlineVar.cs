using System;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineVar : Instruction
	{
		public short Value;
		public Instruction_InlineVar(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 2, op)
		{
			Value = BitConverter.ToInt16(bs, index + 1);
			index += 2;
		}
	}
}
