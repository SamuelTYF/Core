using System;

namespace Collection
{
	public class HuffmanNode : IComparable<HuffmanNode>
	{
		public int Count;

		public int Value;

		public HuffmanNode Parent;

		public HuffmanNode L;

		public HuffmanNode R;

		public List<bool> Codes;

		public HuffmanNode(int value, int count)
		{
			Count = count;
			Value = value;
			Codes = new List<bool>();
		}

		public HuffmanNode(HuffmanNode l, HuffmanNode r)
		{
			Count = l.Count + r.Count;
			(L = l).Parent = ((R = r).Parent = this);
			l.Codes.Add(value: false);
			r.Codes.Add(value: true);
			Value = Count;
			Codes = new List<bool>();
		}

		public int CompareTo(HuffmanNode other)
		{
			return Count.CompareTo(other.Count);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}\t{2}", Value, Count, string.Join(",", Array.ConvertAll(Codes.ToArray(), (bool b) => b ? 1 : 0)));
		}

		public void Lock()
		{
			if (Parent != null)
			{
				Codes.AddList(Parent.Codes);
			}
			if (L != null)
			{
				L.Lock();
			}
			if (R != null)
			{
				R.Lock();
			}
		}
	}
}
