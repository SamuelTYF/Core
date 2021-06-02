using System;
namespace NumericalAnalysis.Root
{
	public static class Muller
	{
		public static Complex FindRoot(Polynomial P, Complex xn2, Complex xn1, double error = 1E-07)
		{
			Complex fn3 = P[xn2];
			Complex fn2 = P[xn1];
			Complex xn3 = (xn2 + xn1) / 2.0;
			Complex fn = P[xn3];
			Complex dx;
			Complex x;
			do
			{
				Complex q = (xn3 - xn1) / (xn1 - xn2);
				Complex A = q * fn - q * (q + 1.0) * fn2 + q * q * fn3;
				Complex B = (2 * q + 1.0) * fn - (q + 1.0) * (q + 1.0) * fn2 + q * q * fn3;
				Complex C = (q + 1.0) * fn;
				dx = (xn3 - xn1) * 2 * C / (B - (B * B - 4 * A * C).Sqrt());
				x = xn3 - dx;
				Complex f = P[x];
				xn2 = xn1;
				fn3 = fn2;
				xn1 = xn3;
				fn2 = fn;
				xn3 = x;
				fn = f;
			}
			while (dx.Norm() > error || - dx.Norm() > error);
			return x;
		}
		public static InterationData<Complex> Monitor(Polynomial P, Complex xn2, Complex xn1, double error = 1E-07)
		{
			InterationData<Complex> data = new("Muller", 10);
			Complex index = new(0.0);
			Complex fn3 = P[xn2];
			Complex fn2 = P[xn1];
			Complex xn3 = (xn2 + xn1) / 2.0;
			Complex fn = P[xn3];
			Complex dx;
			do
			{
				Complex q = (xn3 - xn1) / (xn1 - xn2);
				Complex A = q * fn - q * (q + 1.0) * fn2 + q * q * fn3;
				Complex B = (2 * q + 1.0) * fn - (q + 1.0) * (q + 1.0) * fn2 + q * q * fn3;
				Complex C = (q + 1.0) * fn;
				dx = (xn3 - xn1) * 2 * C / (B + B.Sign() * (B * B - 4 * A * C).Sqrt());
				Complex x = xn3 - dx;
				Complex f = P[x];
				data.Register(index++, xn2, fn3, xn1, fn2, xn3, fn, dx, x, f);
				xn2 = xn1;
				fn3 = fn2;
				xn1 = xn3;
				fn2 = fn;
				xn3 = x;
				fn = f;
			}
			while (dx.Norm() > error || - dx.Norm() > error);
			return data;
		}
		public static void Display(InterationData<Complex> data)
		{
			if (data.Name != "Muller")
				throw new Exception();
			Console.WriteLine(string.Format("{0,-6}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}{8,-15}{9,-15}", "Index", "xn2", "fn2", "xn1", "fn1", "xn", "fn", "dx", "x", "fx"));
			foreach (Complex[] d in data.Data)
				Console.WriteLine(string.Format("{0,-6}{1,-15:0.00000000}{2,-15:0.00000000}{3,-15:0.00000000}{4,-15:0.00000000}{5,-15:0.00000000}{6,-15:0.00000000}{7,-15:0.00000000}{8,-15:0.00000000}{9,-15:0.00000000}", d[0].R, d[1].R, d[2].R, d[3].R, d[4].R, d[5].R, d[6].R, d[7].R, d[8].R, d[9].R));
		}
	}
}
