using Collection;
using System;
namespace NumericalAnalysis
{
	public class FactorHelper
	{
		public static double LearningRate = 0.7;
		public static readonly Random _R = new(DateTime.Now.Millisecond);
		public static double[] Divide(double[] P, double u, double v, out double r1, out double r0)
		{
			int N = P.Length;
			if (N == 1)
			{
				r1 = 0.0;
				r0 = P[0];
				return Array.Empty<double>();
			}
			r1 = P[--N];
			r0 = P[--N];
			double[] Q = new double[N];
			while (N-- > 0)
			{
				Q[N] = r1;
				r1 = r0 - u * r1;
				r0 = P[N] - v * Q[N];
			}
			return Q;
		}
		public static void NDivide(double[] P, double u, double v, out double r1, out double r0)
		{
			int N = P.Length;
			if (N == 1)
			{
				r1 = 0.0;
				r0 = -P[0];
				return;
			}
			r1 = -P[--N];
			r0 = -P[--N];
			while (N-- > 0)
			{
				double i = r1;
				r1 = r0 - u * i;
				r0 = - P[N] - v * i;
			}
		}
		public static void Update(double[] Q, ref double u, ref double v, double r1, double r0)
		{
			NDivide(Q, u, v, out var r1v, out var r0v);
			Divide(new double[3] { 0.0, r0v, r1v }, u, v, out var r1u, out var r0u);
			double det = r1u * r0v - r0u * r1v;
			u += LearningRate * (r0 * r1v - r1 * r0v) / det;
			v += LearningRate * (r1 * r0u - r0 * r1u) / det;
		}
		public static double[] Solve(double[] P, List<Complex> roots, double error = 0.01, double max = 100.0)
		{
			double u = _R.NextDouble();
			double v = _R.NextDouble();
			double[] Q = Divide(P, u, v, out double r1, out double r0);
			while (!(r1 < error) || !(r1 > - error) || !(r0 < error) || !(r0 >  - error))
			{
				if (r1 > max || r0 > max)
				{
					u = _R.NextDouble();
					v = _R.NextDouble();
					Q = Divide(P, u, v, out r1, out r0);
				}
				Update(Q, ref u, ref v, r1, r0);
				Q = Divide(P, u, v, out r1, out r0);
			}
			double delta = u * u - 4.0 * v;
			if (delta < 0.0)
			{
				delta = - delta;
				roots.Add(new((- u) / 2.0, (- Math.Sqrt(delta)) / 2.0));
				roots.Add(new((- u) / 2.0, Math.Sqrt(delta) / 2.0));
			}
			else
			{
				roots.Add(new((Math.Sqrt(delta) - u) / 2.0));
				roots.Add(new((- (Math.Sqrt(delta) + u)) / 2.0));
			}
			return Q;
		}
		public static Complex[] GetRoots(double[] P, double error = 0.01, double max = 100.0)
		{
			List<Complex> roots = new(P.Length - 1);
			while (P.Length > 2)
				P = Solve(P, roots, error, max);
			if (P.Length == 2)
				roots.Add(new( -P[0] / P[1]));
			return roots.ToArray();
		}
	}
}
