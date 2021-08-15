using System;
namespace NumericalAnalysis.Interpolating
{
	public class BulirschStoer
	{
		public int N;
		public int Length;
		public double[] Xs;
		public double[] Ys;
		public BulirschStoer(params Point[] points)
		{
			N = (Length = points.Length) - 1;
			Xs = new double[Length];
			Ys = new double[Length];
			for (int i = 0; i < Length; i++)
			{
				Xs[i] = points[i].X;
				Ys[i] = points[i].Y;
			}
		}
		public BulirschStoer(double[] xs, double[] ys)
		{
			if (xs.Length != ys.Length)
				throw new Exception();
			N = (Length = xs.Length) - 1;
			Xs = xs;
			Ys = ys;
		}
		public PolynomialFraction Interpolate()
		{
			PolynomialFraction[,] r = new PolynomialFraction[Length, Length];
			for (int k = 0; k < Length; k++)
				r[0, k] = new(new(Ys[k]), new(1.0));
			for (int j=0,m = 1; m < Length; m++,j++)
				r[1, j] = new PolynomialFraction(new((Xs[j] - Xs[m]) * Ys[j] * Ys[m]), new(Xs[j] * Ys[j] - Xs[m] * Ys[m], Ys[m] - Ys[j])).Reduce();
			for (int m2 = 2; m2 < Length; m2++)
				for (int i=0,l = m2; l < Length; l++,i++)
					r[m2, i] = (r[m2 - 1, i + 1] + (r[m2 - 1, i + 1] - r[m2 - 1, i]) / (new PolynomialFraction(new(- Xs[i], 1.0), new(- Xs[l], 1.0)) * (1.0 - (r[m2 - 1, i + 1] - r[m2 - 1, i]) / (r[m2 - 1, i + 1] - r[m2 - 2, i + 1])) - 1.0)).Reduce();
			return r[Length - 1, 0];
		}
		public double GetValue(double x)
		{
			double[,] r = new double[Length, Length];
			for (int k = 0; k < Length; k++)
				r[0, k] = Ys[k];
			for (int j=0,m = 1; m < Length; m++,j++)
				r[1, j] = (Xs[j] - Xs[m]) * Ys[j] * Ys[m] / (Xs[j] * Ys[j] - Xs[m] * Ys[m] + (Ys[m] - Ys[j]) * x + 1E-10);
			for (int m2 = 2; m2 < Length; m2++)
				for (int i=0,l = m2; l < Length; l++,i++)
					r[m2, i] = r[m2 - 1, i + 1] + (r[m2 - 1, i + 1] - r[m2 - 1, i]) * (x - Xs[l]) / ((x - Xs[i]) * (1.0 - (r[m2 - 1, i + 1] - r[m2 - 1, i]) / (r[m2 - 1, i + 1] - r[m2 - 2, i + 1])) - (x - Xs[l]));
			return r[Length - 1, 0];
		}
	}
}
