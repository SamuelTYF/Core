using System;
namespace NumericalAnalysis.Root
{
	public class FalsePosition
	{
		public static double FindRoot(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			double xn = x1;
			double f0 = P[x0];
			double xn2;
			do
			{
				xn2 = xn;
				xn = x0 - f0 * (xn2 - x0) / (P[xn2] - f0);
			}
			while (xn - xn2 > error || xn2 - xn > error);
			return xn;
		}
		public static InterationData<double> Monitor(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			InterationData<double> data = new("FalsePosition", 7);
			int index = 0;
			double xn = x1;
			double f0 = P[x0];
			double xn2;
			do
			{
				xn2 = xn;
				double fn1 = P[xn2];
				xn = x0 - f0 * (xn2 - x0) / (fn1 - f0);
				data.Register(index++, x0, f0, xn2, fn1, xn, P[xn]);
			}
			while (xn - xn2 > error || xn2 - xn > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "FalsePosition")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", "Index", "x0", "f(x0)", "xn1", "f(xn1)", "xn", "f(xn)"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6]));
		}
	}
}
