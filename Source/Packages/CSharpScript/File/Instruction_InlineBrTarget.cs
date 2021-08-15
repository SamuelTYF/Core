using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineBrTarget : Instruction, UpdatableInstruction
	{
		public int Value;
		public Instruction Target;
		public Instruction_InlineBrTarget(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = ILIndex + Size + BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}IL_{Value:X}";
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			Target = instructions[Value];
		}
	}
}
