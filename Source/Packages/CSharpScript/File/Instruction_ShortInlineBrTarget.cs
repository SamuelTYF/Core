using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_ShortInlineBrTarget : Instruction, UpdatableInstruction
	{
		public int Value;
		public Instruction Target;
		public Instruction_ShortInlineBrTarget(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 1, op)
		{
			int num = bs[++index];
			if (num >= 128)
				num -= 256;
			Value = ILIndex + Size + num;
		}
		public override string ToString()
		{
			return $"{GetPrefix()}IL_{Value}";
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			Target = instructions[Value];
		}
	}
}
