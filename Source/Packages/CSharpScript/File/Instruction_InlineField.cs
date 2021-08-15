using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineField : Instruction, UpdatableInstruction
	{
		public int Value;
		public FieldOrRef Field;
		public Instruction_InlineField(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
        public override string ToString() => $"{GetPrefix()}{Field}";
        public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			if (Value >> 24 == 4)
			{
				Field = new FieldOrRef(metadata.TildeHeap.FieldTable.Fields[(Value & 0xFFFFFF) - 1]);
				return;
			}
			if (Value >> 24 == 10)
			{
				Field = new FieldOrRef(metadata.TildeHeap.MemberRefTable.MemberRefs[(Value & 0xFFFFFF) - 1]);
				return;
			}
			throw new Exception();
		}
	}
}
