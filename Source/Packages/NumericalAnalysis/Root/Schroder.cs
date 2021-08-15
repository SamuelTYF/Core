using System;
namespace NumericalAnalysis.Root
{
	public static class Schroder
	{
		public static double FindRoot(Polynomial P, double x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				double dd = DD[x];
				double f = P[x];
				dx = f * d / (d * d - f * dd);
				x -= dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
		public static InterationData<double> Monitor(Polynomial P, double x, double k, double error = 1E-07)
		{
			InterationData<double> data = new("Schroder", 7);
			int index = 0;
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				double dd = DD[x];
				double f = P[x];
				dx = f * d / (d * d - f * dd / k);
				data.Register(index++, x, d, dd, f, dx, x -= dx);
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Schroder")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", "Index", "x", "d", "dd", "f(x)", "dx", "x'"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6]));
		}
	}
}
