using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Ldind : ILInstructionCode
	{
		public ElementType Type;
		public ILInstructionCode_Ldind(ElementType type, int offset, int length)
			: base(offset, length, ILCodeFlag.Ldind)
		{
			Type = type;
		}
		public override string Print()
		{
			return $"ldind          {Type}";
		}
	}
}
