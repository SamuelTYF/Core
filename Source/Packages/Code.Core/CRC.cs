using System;
namespace Code.Core
{
	public class CRC
	{
		public int N;
		public bool[] Values;
		public CRC(params int[] indexs)
		{
			N = 0;
			int[] array = indexs;
			foreach (int index in array)
				if (index > N)
					N = index;
			Values = new bool[N + 1];
			array = indexs;
			foreach (int index2 in array)
				Values[N - index2] = true;
		}
		public bool[] Encode(bool[] values)
		{
			bool[] r = new bool[values.Length + N];
			Array.Copy(values, 0, r, 0, values.Length);
			for (int i = 0; i < values.Length; i++)
				if (r[i])
					for (int j = 0; j <= N; j++)
						r[i + j] ^= Values[j];
			Array.Copy(values, 0, r, 0, values.Length);
			return r;
		}
		public bool[] Decode(bool[] values)
		{
			bool[] r = new bool[values.Length - N];
			Array.Copy(values, 0, r, 0, values.Length - N);
			for (int j = 0; j <= r.Length; j++)
				if (values[j])
					for (int k = 0; k <= N; k++)
						values[j + k] ^= Values[k];
			for (int i = r.Length; i < values.Length; i++)
				if (values[i])
					throw new Exception();
			return r;
		}
	}
}
