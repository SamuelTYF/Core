using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdLoc : ILInstructionCode
	{
		public int Index;
		public LocalVar Var;
		public ILInstructionCode_LdLoc(int index, LocalVar[] vars, int offset, int length)
			: base(offset, length, ILCodeFlag.LdLoc)
		{
			Index = index;
			Var = vars[Index];
		}
		public override string Print()
		{
			return "ldloc          " + Var.Name;
		}
	}
}
