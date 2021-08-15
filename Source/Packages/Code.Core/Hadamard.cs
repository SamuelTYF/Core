using System;
using System.Text;

namespace Code.Core
{
	public class Hadamard
	{
		public bool[][] Positive;
		public bool[][] Negetive;
		public int N;
        private static readonly Hadamard H1 = new(
			new bool[1][] { new bool[1] { true } }, 
			new bool[1][] { new bool[1] }, 
			1);
		private static readonly Hadamard H2 = new(
			new bool[2][]
			{
				new bool[2] { true, false },
				new bool[2] { true, true }
			}, 
			new bool[2][]
			{
				new bool[2] { false, true },
				new bool[2]
			}, 
			2);
		public Hadamard(bool[][] positive, bool[][] negetive, int n)
		{
			Positive = positive;
			Negetive = negetive;
			N = n;
		}
		public Hadamard GetNext()
		{
			int j = N << 1;
			bool[][] po = new bool[j][];
			bool[][] ne = new bool[j][];
			for (int i = 0; i < N; i++)
			{
				po[i] = new bool[j];
				po[i + N] = new bool[j];
				ne[i] = new bool[j];
				ne[i + N] = new bool[j];
				Array.Copy(Positive[i], 0, po[i], 0, N);
				Array.Copy(Negetive[i], 0, po[i], N, N);
				Array.Copy(Positive[i], 0, po[i + N], 0, N);
				Array.Copy(Positive[i], 0, po[i + N], N, N);
				Array.Copy(Negetive[i], 0, ne[i], 0, N);
				Array.Copy(Positive[i], 0, ne[i], N, N);
				Array.Copy(Negetive[i], 0, ne[i + N], 0, N);
				Array.Copy(Negetive[i], 0, ne[i + N], N, N);
			}
			return new Hadamard(po, ne, j);
		}
		public static Hadamard Get(int n)
		{
			if (n == 1)
				return H1;
			Hadamard t = H2;
			int i;
			for (i = 2; i < n; i <<= 1)
				t = t.GetNext();
            return i != n ? throw new Exception() : t;
        }
        public static bool Get(int n, int x, int y)
		{
			if (n == 1)
				return true;
			int i = n >> 1;
            return x >= i ? y >= i ? Get(i, x - i, y - i) : Get(i, x - i, y) : y >= i ? !Get(i, x, y - i) : Get(i, x, y);
        }
        public static string Print(int n)
		{
			StringBuilder builder = new();
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
					if (Get(n, i, j))
						builder.Append("1\t");
					else
						builder.Append("-1\t");
				builder.Append("\n");
			}
			return builder.ToString();
		}
		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
					s = (!Positive[i][j]) ? (s + "-1\t") : (s + "1\t");
				s += "\n";
			}
			return s;
		}
	}
}
