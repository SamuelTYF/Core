using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StLoc : ILInstructionCode
	{
		public int Index;
		public LocalVar Var;
		public ILInstructionCode_StLoc(int index, LocalVar[] vars, int offset, int length)
			: base(offset, length, ILCodeFlag.StLoc)
		{
			Index = index;
			Var = vars[Index];
		}
		public override string Print()
		{
			return "stloc          " + Var.Name;
		}
	}
}
