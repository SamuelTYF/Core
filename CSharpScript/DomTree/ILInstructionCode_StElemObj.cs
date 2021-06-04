using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StElemObj : ILInstructionCode
	{
		public TypeDefOrRef Type;
		public ILInstructionCode_StElemObj(Instruction_InlineType type, int offset, int length)
			: base(offset, length, ILCodeFlag.StElemObj)
		{
			Type = new TypeDefOrRef(type.TypeDef, type.TypeRef, type.TypeSpec);
		}
		public override string Print()
		{
			return $"stelem         {Type}";
		}
	}
}
