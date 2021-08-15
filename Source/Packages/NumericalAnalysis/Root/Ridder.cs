using System;
namespace NumericalAnalysis.Root
{
	public class Ridder
	{
		public static double FindRoot(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			double f2 = P[x0];
			double f3 = P[x1];
			if (f2 * f3 > 0.0)
				throw new Exception();
			double x2 = (x0 + x1) / 2.0;
			double tx;
			do
			{
				tx = x2;
				double i = (x0 + x1) / 2.0;
				double fm = P[i];
				double dx = (i - x0) / Math.Sqrt(1.0 - f2 / fm * (f3 / fm));
				x2 = ((!((f2 - f3) * fm > 0.0)) ? (i - dx) : (i + dx));
				double f = P[x2];
				if (f2 * f < 0.0)
				{
					x1 = x2;
					f3 = f;
					if (f2 * fm > 0.0)
					{
						x0 = i;
						f2 = fm;
					}
				}
				else
				{
					x0 = x2;
					f2 = f;
					if (f2 * fm < 0.0)
					{
						x1 = i;
						f3 = fm;
					}
				}
			}
			while (tx - x2 > error || x2 - tx > error);
			return (x1 + x0) / 2.0;
		}
		public static InterationData<double> Monitor(Polynomial P, double x0, double x1, double error = 1E-07)
		{
			InterationData<double> data = new("Ridder", 10);
			int index = 0;
			double f2 = P[x0];
			double f3 = P[x1];
			if (f2 * f3 > 0.0)
				throw new Exception();
			double x2 = (x0 + x1) / 2.0;
			double tx;
			do
			{
				tx = x2;
				double i = (x0 + x1) / 2.0;
				double fm = P[i];
				double dx = (i - x0) / Math.Sqrt(1.0 - f2 / fm * (f3 / fm));
				x2 = ((!((f2 - f3) * fm > 0.0)) ? (i - dx) : (i + dx));
				double f = P[x2];
				data.Register(index++, x0, f2, x1, f3, i, fm, dx, x2, f);
				if (f2 * f < 0.0)
				{
					x1 = x2;
					f3 = f;
					if (f2 * fm > 0.0)
					{
						x0 = i;
						f2 = fm;
					}
				}
				else
				{
					x0 = x2;
					f2 = f;
					if (f2 * fm < 0.0)
					{
						x1 = i;
						f3 = fm;
					}
				}
			}
			while (tx - x2 > error || x2 - tx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Ridder")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}{8,-15}{9,-15}", "Index", "x0", "f(x0)", "x1", "f(x1)", "m", "f(m)", "dx", "x", "f(x)"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-15:0.00000000000}{2,-15:0.00000000000}{3,-15:0.00000000000}{4,-15:0.00000000000}{5,-15:0.00000000000}{6,-15:0.00000000000}{7,-15:0.00000000000}{8,-15:0.00000000000}{9,-15:0.000000000000000}", d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8], d[9]));
		}
	}
}
