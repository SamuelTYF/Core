using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Stind : ILInstructionCode
	{
		public ElementType Type;
		public ILInstructionCode_Stind(ElementType type, int offset, int length)
			: base(offset, length, ILCodeFlag.Stind)
		{
			Type = type;
		}
		public override string Print()
		{
			return $"stind          {Type}";
		}
	}
}
