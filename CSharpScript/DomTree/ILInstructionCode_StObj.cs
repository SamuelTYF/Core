using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StObj : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_StObj(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.StObj)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"stobj          {Type}";
		}
	}
}
