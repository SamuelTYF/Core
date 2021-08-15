using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_IsInst : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_IsInst(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.IsInst)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"isinit         {Type}";
		}
	}
}
