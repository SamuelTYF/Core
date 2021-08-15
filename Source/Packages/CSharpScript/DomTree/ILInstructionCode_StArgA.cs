using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_StArgA : ILInstructionCode
	{
		public int Index;
		public Param Param;
		public ILInstructionCode_StArgA(int index, Param[] p, int offset, int length)
			: base(offset, length, ILCodeFlag.StArgA)
		{
			Index = index;
			Param = p[Index];
		}
		public override string Print()
		{
			return "starga         " + Param.Name;
		}
	}
}
