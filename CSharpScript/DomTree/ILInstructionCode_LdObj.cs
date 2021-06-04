using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdObj : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_LdObj(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdObj)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"ldobj          {Type}";
		}
	}
}
