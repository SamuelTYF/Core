using Collection;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Switch : ILInstructionCode
	{
		public int[] Targets;
		public int[] Sorted;
		public ILInstructionCode_Switch(int[] targets, int offset, int length)
			: base(offset, length, ILCodeFlag.Switch)
		{
			Targets = targets;
			AVL<int, int> aVL = new AVL<int, int>();
			foreach (int num in targets)
			{
				aVL[num] = num;
			}
			Sorted = aVL.LDRSort();
		}
		public override string Print()
		{
			return "switch";
		}
	}
}
