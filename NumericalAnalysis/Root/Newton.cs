using System;
namespace NumericalAnalysis.Root
{
	public static class Newton
	{
		public static double FindRoot(Polynomial P, double x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				if (d == 0.0)
					throw new Exception();
				dx = P[x] / d;
				x -= dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
		public static InterationData<double> Monitor(Polynomial P, double x, double error = 1E-07)
		{
			InterationData<double> data = new("Newton", 7);
			int index = 0;
			Polynomial D = P.GetDerivative();
			double dx;
			do
			{
				double d = D[x];
				if (d == 0.0)
				{
					data.Register(index, x, d, 0.0, 0.0, 0.0, 0.0);
					return data;
				}
				dx = P[x] / d;
				data.Register(index++, x, d, 1.0, P[x], dx, x -= dx);
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static InterationData<double> Monitor(Func<double, double> P, Func<double, double> D, double x, double error = 1E-07)
		{
			InterationData<double> data = new("Newton", 7);
			int index = 0;
			double dx;
			do
			{
				double d = D(x);
				if (d == 0.0)
				{
					data.Register(index, x, d, 0.0, 0.0, 0.0, 0.0);
					return data;
				}
				dx = P(x) / d;
				data.Register(index++, x, d, 1.0, P(x), dx, x -= dx);
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Newton")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-25}", "Index", "x", "d", "IsZero", "f(x)", "dx", "x'"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-25}", d[0], d[1], d[2], d[3] == 0.0, d[4], d[5], d[6]));
		}
	}
}
