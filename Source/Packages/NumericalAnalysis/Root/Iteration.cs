using System;
namespace NumericalAnalysis.Root
{
	public class Iteration
	{
		public static double FindRoot(Polynomial P, double x, double error = 1E-07)
		{
			Polynomial q = new(P.P.Clone() as double[]);
			double t = P.P[P.Order];
			q.P[P.Order] = 0.0;
			double xn;
			do
			{
				xn = x;
				x = Math.Pow((- q[xn]) / t, 1.0 / (double)P.Order);
			}
			while (xn - x > error || x - xn > error);
			return xn;
		}
		public static double FindRoot(Func<double, double> P, double x0, double error = 1E-07)
		{
			double x = x0;
			double xn;
			do
			{
				xn = x;
				x = P(xn);
			}
			while (xn - x > error || x - xn > error);
			return xn;
		}
		public static InterationData<double> Monitor(Polynomial P, double x0, double error = 1E-07)
		{
			Polynomial q = new(P.P.Clone() as double[]);
			double t = P.P[P.Order];
			q.P[P.Order] = 0.0;
			InterationData<double> data = new("Iteration", 4);
			int index = 0;
			double x = x0;
			double xn;
			do
			{
				xn = x;
				double f = P[xn];
				x = Math.Pow((- q[xn]) / t, 1.0 / (double)P.Order);
				data.Register(index++, xn, f, x);
			}
			while (xn - x > error || x - xn > error);
			return data;
		}
		public static InterationData<double> Monitor(Func<double, double> P, double x0, double error = 1E-07)
		{
			double x = x0;
			double index = 0.0;
			InterationData<double> data = new("Iteration", 3);
			double xn;
			do
			{
				xn = x;
				x = P(xn);
				data.Register(index++, x, xn);
			}
			while (xn - x > error || x - xn > error);
			return data;
		}
	}
}
