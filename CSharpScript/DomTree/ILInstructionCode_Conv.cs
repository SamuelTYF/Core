using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Conv : ILInstructionCode
	{
		public ElementType Type;
		public bool Checked;
		public ILInstructionCode_Conv(ElementType type, int offset, int length, bool @checked = false)
			: base(offset, length, ILCodeFlag.Conv)
		{
			Type = type;
			Checked = @checked;
		}
		public override string Print()
		{
			return $"conv           {Type}";
		}
	}
}
