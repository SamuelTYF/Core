using System;
namespace NumericalAnalysis.Interpolating
{
	public class Hermit
	{
		public int N;
		public int Length;
		public double[] Xs;
		public double[] Ys;
		public double[] Ds;
		public HermitBasis Basis;
		public Hermit(params Point[] points)
		{
			N = (Length = points.Length) - 1;
			Xs = new double[Length];
			Ys = new double[Length];
			Ds = new double[Length];
			for (int i = 0; i < Length; i++)
			{
				Xs[i] = points[i].X;
				Ys[i] = points[i].Y;
				Ds[i] = points[i].D;
			}
			Basis = new HermitBasis(Xs);
		}
		public Hermit(double[] xs, double[] ys, double[] ds)
		{
			if (xs.Length != ys.Length || xs.Length != ds.Length)
				throw new Exception();
			N = (Length = xs.Length) - 1;
			Xs = xs;
			Ys = ys;
			Ds = ds;
			Basis = new HermitBasis(Xs);
		}
		public Polynomial Interpolate()
		{
			Polynomial r = Polynomial.Zero;
			for (int i = 0; i < Length; i++)
			{
				Basis.GetBasis(i, out var a, out var b);
				r += Ys[i] * a + Ds[i] * b;
			}
			return r;
		}
	}
}
