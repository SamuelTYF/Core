using System;
using Collection;
namespace Code.Core
{
	public class HammingCode
	{
		public int D;
		public int N;
		public int K;
		public List<int> CheckPoints;
		public List<List<int>> Indexs;
		public HammingCode(int n)
		{
			N = n;
			int k = 0;
			int k2 = 1;
			CheckPoints = new(16);
			for (; k2 - 1 < n + k; k++)
			{
				CheckPoints.Add(k2);
				k2 <<= 1;
			}
			K = k;
			D = n + k;
			Indexs = new();
			foreach (int p in CheckPoints)
			{
				List<int> indexs = new();
				for (int i = p; i < D; i += p)
					for (int j = 0; j < p; j++)
						indexs.Add(i++ - 1);
				Indexs.Add(indexs);
			}
		}
		public bool[] Encode(bool[] values)
		{
			if (values.Length > N)
				throw new Exception();
			bool[] r = new bool[D];
			for (int j = 0; j < K; j++)
			{
				int p = CheckPoints[j];
				Array.Copy(values, p - 1 - j, r, p, p - 1);
			}
			for (int i = 0; i < K; i++)
			{
				int p2 = CheckPoints[i];
				bool t = true;
				foreach (int k in Indexs[i])
					t ^= r[k];
				r[p2 - 1] ^= t;
			}
			return r;
		}
		public bool[] Decode(bool[] values)
		{
			if (values.Length != D)
				throw new Exception();
			bool[] r = new bool[N];
			for (int j = 0; j < K; j++)
			{
				bool t = true;
				foreach (int k in Indexs[j])
					t ^= values[k];
				if (t)
					throw new Exception();
			}
			for (int i = 0; i < K; i++)
			{
				int p = CheckPoints[i];
				Array.Copy(values, p, r, p - 1 - i, p - 1);
			}
			return r;
		}
	}
}
