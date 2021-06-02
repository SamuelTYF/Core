using System;
namespace NumericalAnalysis
{
	public class Bernoulli
	{
		public static double FindRoot(Polynomial P, int t)
		{
			double[] r = new double[P.Order + t];
			for (int j = 0; j < P.Order; j++)
				r[j] = 1.0;
			double q = P.P[P.Order];
			for (int i = 0; i < t; i++)
			{
				double s = 0.0;
				for (int l = 0; l < P.Order; l++)
					s += P.P[l] * r[i + l];
				r[i + P.Order] = - s / q;
				for (int k = P.Order; k >= 0; k--)
					r[i + k] /= r[i];
				Console.WriteLine(r[i + P.Order] / r[i + P.Order - 1]);
			}
			return -1.0;
		}
	}
}
