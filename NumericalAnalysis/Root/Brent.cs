using System;
namespace NumericalAnalysis.Root
{
	public class Brent
	{
		public static double FindRoot(Polynomial P, double a, double b, double error = 1E-07)
		{
			double fa = P[a];
			double fb = P[b];
			double c = a;
			double fc = fa;
			double dx;
			do
			{
				if (Math.Abs(fc) < Math.Abs(fb))
				{
					a = b;
					b = c;
					c = a;
					fa = fb;
					fb = fc;
					fc = fa;
				}
				if (a == c)
					dx = (- fb) * (b - a) / (fb - fa);
				else
				{
					double r = fb / fc;
					double s = fb / fa;
					double t = fa / fc;
					dx = s * (t * (r - t) * (c - b) - (1.0 - r) * (b - a)) / ((r - 1.0) * (s - 1.0) * (t - 1.0));
				}
				a = b;
				fa = fb;
				b += dx;
				fb = P[b];
				if (fb > 0.0 == fc > 0.0)
				{
					c = a;
					fc = fa;
				}
			}
			while (dx > error || - dx > error);
			return b;
		}
		public static InterationData<double> Monitor(Polynomial P, double a, double b, double error = 1E-07)
		{
			InterationData<double> data = new("Brent", 11);
			int index = 0;
			double fa = P[a];
			double fb = P[b];
			double c = a;
			double fc = fa;
			double dx;
			do
			{
				if (Math.Abs(fc) < Math.Abs(fb))
				{
					a = b;
					b = c;
					c = a;
					fa = fb;
					fb = fc;
					fc = fa;
				}
				if (a == c)
				{
					dx = - fb * (b - a) / (fb - fa);
					data.Register(index++, a, fa, b, fb, c, fc, 0.0, dx, b + dx, P[b + dx]);
				}
				else
				{
					double r = fb / fc;
					double s = fb / fa;
					double t = fa / fc;
					dx = s * (t * (r - t) * (c - b) - (1.0 - r) * (b - a)) / ((r - 1.0) * (s - 1.0) * (t - 1.0));
					data.Register(index++, a, fa, b, fb, c, fc, 1.0, dx, b + dx, P[b + dx]);
				}
				a = b;
				fa = fb;
				b += dx;
				fb = P[b];
				if (fb > 0.0 == fc > 0.0)
				{
					c = a;
					fc = fa;
				}
			}
			while (dx > error || - dx > error);
			return data;
		}
		public static void Display(InterationData<double> data)
		{
			if (data.Name != "Brent")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}{7,-10}{8,-25}{9,-25}{10,-25}", "Index", "a", "f(a)", "b", "f(b)", "c", "f(c)", "Approach", "dx", "b'", "f(b')"));
			foreach (double[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-25}{2,-25}{3,-25}{4,-25}{5,-25}{6,-25}{7,-10}{8,-25}{9,-25}{10,-25}", d[0], d[1], d[2], d[3], d[4], d[5], d[6], (d[7] == 1.0) ? "Quadratic" : "Linear", d[8], d[9], d[10]));
		}
	}
}
