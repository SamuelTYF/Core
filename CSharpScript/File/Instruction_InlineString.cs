using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineString : Instruction, UpdatableInstruction
	{
		public int Value;
		public string String;
		public Instruction_InlineString(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return GetPrefix() + "\"" + String + "\"";
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			if (Value >> 24 != 112)
				throw new Exception();
			String = metadata.USHeap.Strings[Value & 0xFFFFFF];
		}
	}
}
