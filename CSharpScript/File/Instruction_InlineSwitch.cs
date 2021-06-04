using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineSwitch : Instruction, UpdatableInstruction
	{
		public int Value;
		public int[] Labels;
		public Instruction[] Branches;
		public Instruction_InlineSwitch(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
			Labels = new int[Value];
			Size += 4 * Value;
			for (int i = 0; i < Value; i++)
			{
				Labels[i] = ILIndex + Size + BitConverter.ToInt32(bs, index + 1);
				index += 4;
			}
		}
		public override string ToString()
		{
			return string.Format("{0}({1}){{{2}}}", GetPrefix(), Value, string.Join(",", Array.ConvertAll(Labels, (int label) => $"IL_{label}")));
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			Branches = new Instruction[Value];
			for (int i = 0; i < Value; i++)
			{
				Branches[i] = instructions[Labels[i]];
			}
		}
	}
}
