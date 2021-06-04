using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdElemObj : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_LdElemObj(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdElemObj)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"ldelem         {Type}";
		}
	}
}
