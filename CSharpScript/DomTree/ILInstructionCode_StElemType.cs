using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StElemType : ILInstructionCode
	{
		public ElementType Type;
		public ILInstructionCode_StElemType(ElementType type, int offset, int length)
			: base(offset, length, ILCodeFlag.StElemType)
		{
			Type = type;
		}
		public override string Print()
		{
			return $"stelem         {Type}";
		}
	}
}
