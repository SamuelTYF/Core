using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StArg : ILInstructionCode
	{
		public int Index;
		public Param Param;
		public ILInstructionCode_StArg(int index, Param[] p, int offset, int length)
			: base(offset, length, ILCodeFlag.StArg)
		{
			Index = index;
			Param = p[Index];
		}
		public override string Print()
		{
			return "starg          " + Param.Name;
		}
	}
}
