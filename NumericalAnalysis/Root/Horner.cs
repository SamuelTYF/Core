using System;
namespace NumericalAnalysis.Root
{
	public static class Horner
	{
		public static Polynomial Translate(Polynomial P, double x)
		{
			double[] r = new double[P.Order + 1];
			Array.Copy(P.P, 0, r, 0, r.Length);
			for (int i = 0; i < P.Order; i++)
			{
				for (int j = P.Order - 1; j >= i; j--)
					r[j] += r[j + 1] * x;
			}
			return new(r);
		}
		public static Polynomial Scale(Polynomial P, double s)
		{
			double t = 1.0;
			double[] r = new double[P.Order + 1];
			int i = P.Order;
			while (i >= 0)
			{
				r[i] = P.P[i] * t;
				i--;
				t *= s;
			}
			return new(r);
		}
		public static int FindRange(Polynomial P, double x, out bool zero)
		{
			double t = Newton.FindRoot(P, x, 0.1);
			int x2 = (int)t;
			if (t < 0.0)
				x2--;
			double f0 = P[x2];
			if (f0 == 0.0)
			{
				zero = true;
				return x2;
			}
			double f1 = P[x2 + 1];
			if (f1 == 0.0)
			{
				zero = true;
				return x2 + 1;
			}
			if (f0 * f1 < 0.0)
			{
				zero = false;
				return x2;
			}
			if (f1 * P[x2 + 2] < 0.0)
			{
				zero = false;
				return x2 + 1;
			}
			throw new Exception();
		}
		public static string FindRoot(Polynomial P, double x, int count)
		{
			int t = FindRange(P, x, out bool zero);
			if (t < 0)
			{
				P = Scale(P, -1.0);
				t = -t - 1;
			}
			string s = t + ".";
			while (count-- > 0 && !zero)
			{
				P = Scale(Translate(P, t), 10.0);
				t = FindRange(P, 0.0, out zero);
				s += t;
			}
			return s;
		}
	}
}
