using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdElemType : ILInstructionCode
	{
		public ElementType Type;
		public ILInstructionCode_LdElemType(ElementType type, int offset, int length)
			: base(offset, length, ILCodeFlag.LdElemType)
		{
			Type = type;
		}
		public override string Print()
		{
			return $"ldelem         {Type}";
		}
	}
}
