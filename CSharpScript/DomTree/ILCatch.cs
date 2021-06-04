using System;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public sealed class ILCatch : ILBlock
	{
		public TypeDefOrRef Type;
		public ILCatch(int offset, int length)
			: base(offset, length, ILCodeFlag.ILCatch)
		{
		}
		public override string Print(int tabs = 0)
		{
			return string.Concat("catch".PadLeft(tabs + 5, '\t') + Type.ToString() + "\n", string.Join("\n", Array.ConvertAll(Codes, (ILCode code) => code.Print(tabs + 1))));
		}
	}
}
