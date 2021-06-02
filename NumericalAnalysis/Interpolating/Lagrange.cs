using System;
namespace NumericalAnalysis.Interpolating
{
	public class Lagrange
	{
		public int N;
		public int Length;
		public double[] Xs;
		public double[] Ys;
		public LagrangeBasis Basis;
		public Lagrange(params Point[] points)
		{
			N = (Length = points.Length) - 1;
			Xs = new double[Length];
			Ys = new double[Length];
			for (int i = 0; i < Length; i++)
			{
				Xs[i] = points[i].X;
				Ys[i] = points[i].Y;
			}
			Basis = new LagrangeBasis(Xs);
		}
		public Lagrange(double[] xs, double[] ys)
		{
			if (xs.Length != ys.Length)
				throw new Exception();
			N = (Length = xs.Length) - 1;
			Xs = xs;
			Ys = ys;
			Basis = new LagrangeBasis(xs);
		}
		public Polynomial Interpolate()
		{
			Polynomial r = Polynomial.Zero;
			for (int i = 0; i < Length; i++)
				r += Ys[i] * Basis.L(i);
			return r;
		}
		public Frequency InterpolateF()
		{
			if (Basis.Total == null)
				Basis.ComputePi();
			Frequency r = Frequency.Zero(N + 1);
			for (int i = 0; i < Length; i++)
			{
				Frequency b = Basis.F(i);
				b *= 1.0 / b.IFFT_2_Time_Position(N + 1)[Xs[i]];
				r += b * Ys[i];
			}
			return r;
		}
	}
}
