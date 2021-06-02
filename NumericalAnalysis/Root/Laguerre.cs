using System;
namespace NumericalAnalysis.Root
{
	public static class Laguerre
	{
		public static double FindRoot(Polynomial P, int n, double x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				double dd = DD[x];
				double f = P[x];
				double g = d / f;
				double h = g * g - dd / f;
				dx = (double)n / (g + (double)((g >= 0.0) ? 1 : (-1)) * Math.Sqrt((double)(n - 1) * ((double)n * h - g * g)));
				x -= dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
		public static InterationData<double> Monitor(Polynomial P, int n, double x, double error = 1E-07)
		{
			InterationData<double> data = new("Laguerre", 9);
			int index = 0;
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				double dd = DD[x];
				double f = P[x];
				double g = d / f;
				double h = g * g - dd / f;
				dx = (double)n / (g + (double)((g >= 0.0) ? 1 : (-1)) * Math.Sqrt((double)(n - 1) * ((double)n * h - g * g)));
				data.Register(index++, x, d, dd, f, g, h, dx, x -= dx);
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Laguerre")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}{7,-25}{8,-25}", "Index", "x", "d", "dd", "f(x)", "g", "h", "dx", "x'"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}{7,-25}{8,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8]));
		}
	}
}
