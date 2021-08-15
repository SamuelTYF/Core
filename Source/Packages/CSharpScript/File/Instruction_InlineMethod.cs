using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineMethod : Instruction, UpdatableInstruction
	{
		public int Value;
		public MethodDef MethodDef;
		public MemberRef MemberRef;
		public MethodSpec MethodSpec;
		public Instruction_InlineMethod(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return (Value >> 24) switch
			{
				6 => $"{GetPrefix()}{MethodDef}", 
				10 => $"{GetPrefix()}{MemberRef}", 
				43 => $"{GetPrefix()}{MethodSpec}", 
				_ => throw new Exception(), 
			};
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			switch (Value >> 24)
			{
			case 6:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(Value & 0xFFFFFF) - 1];
				break;
			case 10:
				MemberRef = metadata.TildeHeap.MemberRefTable.MemberRefs[(Value & 0xFFFFFF) - 1];
				break;
			case 43:
				MethodSpec = metadata.TildeHeap.MethodSpecTable.MethodSpecs[(Value & 0xFFFFFF) - 1];
				break;
			default:
				throw new Exception();
			}
		}
	}
}
