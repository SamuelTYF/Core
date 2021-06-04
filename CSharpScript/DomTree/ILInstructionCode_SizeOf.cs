using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_SizeOf : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_SizeOf(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.SizeOf)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"sizeof         {Type}";
		}
	}
}
