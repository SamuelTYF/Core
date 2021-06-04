using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class Instruction_InlineTok : Instruction, UpdatableInstruction
	{
		public int Value;
		public MemberRef MemberRef;
		public TypeRef TypeRef;
		public TypeDef TypeDef;
		public TypeSpec TypeSpec;
		public Field Field;
		public Instruction_InlineTok(int ilIndex, Operator op, byte[] bs, ref int index)
			: base(ilIndex, 4, op)
		{
			Value = BitConverter.ToInt32(bs, index + 1);
			index += 4;
		}
		public override string ToString()
		{
			return (Value >> 24) switch
			{
				10 => $"{GetPrefix()}{MemberRef}", 
				1 => $"{GetPrefix()}{TypeRef}", 
				2 => $"{GetPrefix()}{TypeDef}", 
				4 => $"{GetPrefix()}{TypeSpec}", 
				27 => $"{GetPrefix()}{Field}", 
				_ => throw new Exception(), 
			};
		}
		public void Update(AVL<int, Instruction> instructions, MetadataRoot metadata)
		{
			switch (Value >> 24)
			{
			case 1:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(Value & 0xFFFFFF) - 1];
				break;
			case 2:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(Value & 0xFFFFFF) - 1];
				break;
			case 4:
				Field = metadata.TildeHeap.FieldTable.Fields[(Value & 0xFFFFFF) - 1];
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
