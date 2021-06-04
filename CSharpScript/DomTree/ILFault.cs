using System;
namespace CSharpScript.DomTree
{
	public sealed class ILFault : ILBlock
	{
		public ILFault(int offset, int length)
			: base(offset, length, ILCodeFlag.ILFault)
		{
		}
		public override string Print(int tabs = 0)
		{
			return "fault\n".PadLeft(tabs + 6, '\t') + string.Join("\n", Array.ConvertAll(Codes, (ILCode code) => code.Print(tabs + 1)));
		}
	}
}
