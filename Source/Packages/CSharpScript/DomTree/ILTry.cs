using System;
using Collection;
namespace CSharpScript.DomTree
{
	public sealed class ILTry : ILBlock
	{
		public List<ILCatch> Catches;
		public ILFinally Finally;
		public ILFault Fault;
        public ILTry(int offset, int length)
            : base(offset, length, ILCodeFlag.ILTry) => Catches = new();
        public override string Print(int tabs = 0) => "try\n".PadLeft(tabs + 4, '\t') + string.Join("\n", Array.ConvertAll(Codes, (ILCode code) => code.Print(tabs + 1)));
    }
}
