using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Constrained : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_Constrained(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.Constrained)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"constrained    {Type}";
		}
	}
}
