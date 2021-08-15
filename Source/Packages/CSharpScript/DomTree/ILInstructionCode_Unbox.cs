using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Unbox : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_Unbox(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.Unbox)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"unbox          {Type}";
		}
	}
}
