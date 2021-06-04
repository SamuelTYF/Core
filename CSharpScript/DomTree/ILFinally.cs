using System;
namespace CSharpScript.DomTree
{
	public sealed class ILFinally : ILBlock
	{
		public ILFinally(int offset, int length)
			: base(offset, length, ILCodeFlag.ILFinally)
		{
		}
		public override string Print(int tabs = 0)
		{
			return "finally\n".PadLeft(tabs + 8, '\t') + string.Join("\n", Array.ConvertAll(Codes, (ILCode code) => code.Print(tabs + 1)));
		}
	}
}
