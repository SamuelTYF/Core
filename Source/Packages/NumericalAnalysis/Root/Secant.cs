using System;
namespace NumericalAnalysis.Root
{
	public class Secant
	{
		public static double FindRoot(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			double xn2 = x0;
			double xn = x1;
			double fn1 = P[x0];
			do
			{
				double xn3 = xn2;
				xn2 = xn;
				double fn2 = fn1;
				fn1 = P[xn2];
				xn = xn2 - fn1 * (xn2 - xn3) / (fn1 - fn2);
			}
			while (xn - xn2 > error || xn2 - xn > error);
			return xn;
		}
		public static InterationData<double> Monitor(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			InterationData<double> data = new("Secant", 7);
			int index = 0;
			double xn2 = x0;
			double xn = x1;
			double fn1 = P[x0];
			do
			{
				double xn3 = xn2;
				xn2 = xn;
				double fn2 = fn1;
				fn1 = P[xn2];
				xn = xn2 - fn1 * (xn2 - xn3) / (fn1 - fn2);
				data.Register(index++, xn3, fn2, xn2, fn1, xn, P[xn]);
			}
			while (xn - xn2 > error || xn2 - xn > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Secant")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", "Index", "xn2", "f(xn2)", "xn1", "f(xn1)", "xn", "f(xn)"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6]));
		}
	}
}
