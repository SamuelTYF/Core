using System;
namespace NumericalAnalysis.Root
{
	public static class Householder
	{
		public static double FindRoot(Polynomial P, double x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				if (d == 0.0)
					throw new Exception();
				double dd = DD[x];
				double f = P[x];
				dx = (1.0 + f * dd / (2.0 * d * d)) * f / d;
				x -= dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
		public static InterationData<double> Monitor(Polynomial P, double x, double error = 1E-07)
		{
			InterationData<double> data = new("Householder", 8);
			int index = 0;
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				if (d == 0.0)
				{
					data.Register(index, x, d, 0.0, 0.0, 0.0, 0.0, 0.0);
					return data;
				}
				double dd = DD[x];
				double f = P[x];
				dx = (1.0 + f * dd / (2.0 * d * d)) * f / d;
				data.Register(index++, x, d, 1.0, dd, f, dx, x -= dx);
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Householder")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-25}{7,-25}", "Index", "x", "d", "IsZero", "dd", "f(x)", "dx", "x'"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-25}{7,-25}", d[0], d[1], d[2], d[3] == 0.0, d[4], d[5], d[6], d[7]));
		}
	}
}
