using System;
namespace CSharpScript.DomTree
{
	public struct ILCodeInfo : IComparable<ILCodeInfo>
	{
		public int Index;
		public int Offset;
		public int Length;
		public ILCodeFlag Flag;
		public int CompareTo(ILCodeInfo other)
		{
			int num = other.Offset.CompareTo(Offset);
			if (num == 0)
				return other.Length.CompareTo(Length);
			return num;
		}
	}
}
