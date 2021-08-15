using System;
namespace NumericalAnalysis.Interpolating
{
	public class Neville
	{
		public int N;
		public int Length;
		public double[] Xs;
		public double[] Ys;
		public Neville(params Point[] points)
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
		public Neville(double[] xs, double[] ys)
		{
			if (xs.Length != ys.Length)
				throw new Exception();
			N = (Length = xs.Length) - 1;
			Xs = xs;
			Ys = ys;
		}
		public Polynomial Interpolate()
		{
			int k = Length;
			Polynomial[] ps = new Polynomial[Length];
			for (int j = 0; j < Length; j++)
				ps[j] = new(Ys[j]);
			while (--k > 0)
				for (int i = 0; i < k; i++)
				{
					double xi = Xs[i];
					double xj = Xs[Length - k + i];
					ps[i] += (ps[i + 1] - ps[i]) * new Polynomial(xi / (xi - xj), 1.0 / (xj - xi));
				}
			return ps[0];
		}
	}
}
