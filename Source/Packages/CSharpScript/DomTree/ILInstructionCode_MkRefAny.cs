using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_MkRefAny : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_MkRefAny(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.MkRefAny)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"mkrefany        {Type}";
		}
	}
}
