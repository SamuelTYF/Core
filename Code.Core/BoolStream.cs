using Collection;
namespace Code.Core
{
	public class BoolStream
	{
		public List<long> Values;
		public int Position;
		public int BitPosition;
		public int BitLength;
		public int Length;
		public const int BitsPerValue = 64;
		private static readonly long[] Mask;
		static BoolStream()
		{
			Mask = new long[64];
			long a = 1L;
			for (int i = 0; i < 64; i++)
			{
				Mask[i] = a;
				a <<= 1;
			}
		}
		public BoolStream()
		{
			Values = new List<long>(16);
			Position = 0;
			BitPosition = 0;
			BitLength = 0;
			Length = 0;
		}
		public void Push(bool[] values, int index, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int t = BitLength++ % 64;
				if (t == 0)
					Values.Add(0L);
				if (values[index + i])
				{
					List<long> values2 = Values;
					int num = Values.Length - 1;
					values2[num]=values2[num] | Mask[t];
				}
			}
		}
		public bool[] Get(int length)
		{
			bool[] r = new bool[length];
			for (int i = 0; i < length; i++)
			{
				int t = BitPosition++ % 64;
				r[i] = (Values[Position] & Mask[t]) == Mask[t];
				if (t + 1 == 64)
					Position++;
			}
			return r;
		}
	}
}
