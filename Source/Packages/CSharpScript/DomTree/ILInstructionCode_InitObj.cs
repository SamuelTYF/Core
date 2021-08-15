using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_InitObj : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_InitObj(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.InitObj)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"initobj        {Type}";
		}
	}
}
