using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_NewArray : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_NewArray(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.NewArray)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"newarr         {Type}";
		}
	}
}
