using System;
namespace NumericalAnalysis.Root
{
	public class Lambert
	{
		public double FindRoot(double d, double r, double x, double error)
		{
			double dx;
			do
			{
				double xd = Math.Pow(x, d);
				dx = ((d - 1.0) * xd + (d + 1.0) * r) / ((d + 1.0) * xd + (d - 1.0) * r);
				x += dx;
			}
			while (dx > error || - dx > error);
			return x;
		}
	}
}
