using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineSig : Instruction, UpdatableInstruction
	{
		public int Value;
		public StandAloneSig StandAloneSig;
		public Instruction_InlineSig(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			if (Value >> 24 == 17)
			{
				StandAloneSig = metadata.TildeHeap.StandAloneSigTable.StandAloneSigs[(Value & 0xFFFFFF) - 1];
				if (StandAloneSig != null)
					throw new Exception();
				return;
			}
			throw new Exception();
		}
	}
}
