using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Box : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_Box(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.Box)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"box            {Type}";
		}
	}
}
