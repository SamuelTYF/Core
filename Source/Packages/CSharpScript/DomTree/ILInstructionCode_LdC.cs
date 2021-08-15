using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdC : ILInstructionCode
	{
		public ElementType Type;
		public object Value;
		public ILInstructionCode_LdC(ElementType type, object value, int offset, int length)
			: base(offset, length, ILCodeFlag.LdC)
		{
			Type = type;
			Value = value;
		}
		public override string Print()
		{
			return $"ldc            {Value}";
		}
	}
}
