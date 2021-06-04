using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdElemA : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_LdElemA(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdElemA)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"ldelema        {Type}";
		}
	}
}
