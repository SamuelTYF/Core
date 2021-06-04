using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_CastClass : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_CastClass(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.CastClass)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"castclass      {Type}";
		}
	}
}
