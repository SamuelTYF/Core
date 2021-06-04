using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdArgA : ILInstructionCode
	{
		public int Index;
		public Param Param;
		public ILInstructionCode_LdArgA(int index, Param[] p, int offset, int length)
			: base(offset, length, ILCodeFlag.LdArgA)
		{
			Index = index;
			Param = p[Index];
		}
		public override string Print()
		{
			return "ldarga         " + Param.Name;
		}
	}
}
