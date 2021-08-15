using System;
namespace NumericalAnalysis.Root
{
	public static class HalleyIrrational
	{
		public static Complex FindRoot(Polynomial P, Complex x, double error = 1E-07)
		{
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			Complex dx;
			do
			{
				Complex d = D[x];
				Complex dd = DD[x];
				Complex f = P[x];
				dx = 2 * f / (d + (d * d - 2 * f * dd).Sqrt());
				x -= dx;
			}
			while (dx.Norm() > error || - dx.Norm() > error);
			return x;
		}
		public static InterationData<Complex> Monitor(Polynomial P, Complex x, double error = 1E-07)
		{
			InterationData<Complex> data = new("HalleyIrrational", 7);
			Complex index = new(0.0);
			Polynomial D = P.GetDerivative();
			Polynomial DD = D.GetDerivative();
			Complex dx;
			do
			{
				Complex d = D[x];
				Complex dd = DD[x];
				Complex f = P[x];
				dx = 2 * f / (d + (d * d - 2 * f * dd).Sqrt());
				data.Register(index++, x, d, dd, f, dx, x -= dx);
			}
			while (dx.Norm() > error || - dx.Norm() > error);
			return data;
		}
		public static void Display(InterationData<Complex> data)
		{
			if (data.Name != "HalleyHalleyIrrational")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", "Index", "x", "d", "dd", "f(x)", "dx", "x'"));
			foreach (Complex[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6]));
		}
	}
}
