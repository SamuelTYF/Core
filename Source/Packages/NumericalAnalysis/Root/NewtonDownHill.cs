using System;
namespace NumericalAnalysis.Root
{
	public static class NewtonDownHill
	{
		public static double FindRoot(Polynomial P, double x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			double dx;
			do
			{
				double f = P[x];
				double d = D[x];
				if (d == 0.0)
					throw new Exception();
				dx = f / d;
				while (Math.Abs(P[x - dx]) >= Math.Abs(f))
				{
					dx /= 2.0;
				}
				x -= dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
		public static InterationData<double> Monitor(Polynomial P, double x, double error = 1E-07)
		{
			InterationData<double> data = new("NewtonDownHill", 9);
			int index = 0;
			int i = 0;
			Polynomial D = P.GetDerivative();
			double t;
			do
			{
				double f = P[x];
				double d = D[x];
				if (d == 0.0)
				{
					data.Register(index, x, d, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
					return data;
				}
				double dx;
				t = (dx = P[x] / d);
				while (Math.Abs(P[x - t]) >= Math.Abs(f))
				{
					t /= 2.0;
					i++;
				}
				data.Register(index++, x, d, 1.0, f, dx, i, t, x -= t);
			}
			while (t > error || - t > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "NewtonDownHill")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-5}{7,-25}{8,-25}", "Index", "x", "d", "IsZero", "f(x)", "dx", "k", "t", "x'"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-8}{4,-25}{5,-25}{6,-5}{7,-25}{8,-25}", d[0], d[1], d[2], d[3] == 0.0, d[4], d[5], d[6], d[7], d[8]));
		}
	}
}
