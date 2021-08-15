using System;
namespace NumericalAnalysis.Root
{
	public class Graeffe
	{
		public static double[] FindRoot(Polynomial P, int v)
		{
			Polynomial fp = P;
			for (int t = 0; t < v; t++)
			{
				double[] r = new double[fp.Order + 1];
				for (int j = 0; j < r.Length; j++)
					r[j] = ((j % 2 == 0) ? P.P[j] : (- P.P[j]));
				Polynomial f = fp * new Polynomial(r);
				double[] bs = new double[P.Order + 1];
				for (int i = 0; i < bs.Length; i++)
					bs[i] = f.P[i << 1];
				fp = new(bs);
				Console.WriteLine(fp);
			}
			double[] ys = new double[P.Order];
			for (int k = P.Order; k > 0; k--)
				ys[k - 1] = Math.Pow((- fp.P[k - 1]) / fp.P[k], 1.0 / (double)(v + 1));
			return ys;
		}
	}
}
