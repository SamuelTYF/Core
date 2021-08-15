using System;
namespace NumericalAnalysis.Minimum
{
	public class Simplex : IMinimum
	{
		public class Point : IComparable<Point>
		{
			public double[] Xs;
			public double F;
			public Point(double[] xs, double f)
			{
				Xs = xs;
				F = f;
			}
			public int CompareTo(Point other)
			{
				int r = F.CompareTo(other.F);
				if (r == 0)
				{
					for (int i = 0; i < Xs.Length; i++)
					{
						r = Xs[i].CompareTo(other.Xs[i]);
						if (r != 0) return r;
					}
					return 0;
				}
				else return r;
			}
		}
		public Point[] PS;
		public Simplex(Func<double[], double> f, double[] xs,double[] hs, double omega, double error)
			: base(f, xs,hs, omega, error)
		{
			PS = new Point[Length + 1];
			PS[^1] = new Point(Xs, F(Xs));
			for(int i=0;i<Length;i++)
            {
				double[] txs = new double[Length];
				Array.Copy(Xs, 0, txs, 0, Length);
				txs[i] += Hs[i];
				PS[i] = new Point(txs, F(txs));
            }
			Array.Sort(PS);
		}
		public override double Run(out double e)
		{
			double min;
			e = double.PositiveInfinity;
			do
			{
				min = PS[0].F;
				Xs = new double[Length];
				for (int i = 0; i < Length; i++)
				{
					Hs[i] *= Omega;
					Xs[i] = PS[0].Xs[i] * 2 - PS[^1].Xs[i];
				}
				PS[^1] = new Point(Xs, F(Xs));
				for (int i = 0; i < Length; i++)
				{
					double[] txs = new double[Length];
					Array.Copy(Xs, 0, txs, 0, Length);
					txs[i] += Hs[i];
					PS[i] = new Point(txs, F(txs));
				}
				Array.Sort(PS);
				e = Math.Abs((min - PS[0].F) / PS[0].F);
			}
			while (e >= Error);
			return PS[0].F;
		}
		public override string GetName() => "Simplex";
	}
}
