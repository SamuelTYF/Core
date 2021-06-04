using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdArg : ILInstructionCode
	{
		public int Index;
		public Param Param;
		public ILInstructionCode_LdArg(int index, Param[] p, int offset, int length)
			: base(offset, length, ILCodeFlag.LdArg)
		{
			Index = index;
			Param = p[Index];
		}
		public override string Print()
		{
			return "ldarg          " + Param.Name;
		}
	}
}
