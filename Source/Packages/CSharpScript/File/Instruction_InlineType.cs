using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineType : Instruction, UpdatableInstruction
	{
		public int Value;
		public TypeDef TypeDef;
		public TypeRef TypeRef;
		public TypeSpec TypeSpec;
		public Instruction_InlineType(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return (Value >> 24) switch
			{
				2 => $"{GetPrefix()}{TypeDef}", 
				1 => $"{GetPrefix()}{TypeRef}", 
				27 => $"{GetPrefix()}{TypeSpec}", 
				_ => throw new Exception(), 
			};
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			switch (Value >> 24)
			{
			case 2:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(Value & 0xFFFFFF) - 1];
				break;
			case 1:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(Value & 0xFFFFFF) - 1];
				break;
			case 27:
				TypeSpec = metadata.TildeHeap.TypeSpecTable.TypeSpecs[(Value & 0xFFFFFF) - 1];
				break;
			default:
				throw new Exception();
			}
		}
	}
}
