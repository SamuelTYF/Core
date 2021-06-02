using System;
namespace NumericalAnalysis.Interpolating
{
	public class Newton
	{
		public int N;
		public int Length;
		public double[] Xs;
		public double[] Ys;
		public Newton(params Point[] points)
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
		public Newton(double[] xs, double[] ys)
		{
			if (xs.Length != ys.Length)
				throw new Exception();
			N = (Length = xs.Length) - 1;
			Xs = xs;
			Ys = ys;
		}
		public Polynomial Interpolate()
		{
			double[] dd = new double[Length];
			Array.Copy(Ys, 0, dd, 0, Length);
			Polynomial r = Polynomial.Zero;
			Polynomial pi = new(1.0);
			for (int i = 0; i < Length; i++)
			{
				r += dd[0] * pi;
				pi *= new Polynomial(- Xs[i], 1.0);
				int j = i + 1;
				int k = 0;
				while (j < Length)
				{
					dd[k] = (dd[k] - dd[k + 1]) / (Xs[k] - Xs[j]);
					j++;
					k++;
				}
			}
			return r;
		}
	}
}
