using System;
using Collection;
namespace NumericalAnalysis
{
	public class Polynomial
	{
		public const double PError = 1E-10;
		public const double NError = -1E-10;
		public int Order;
		public double[] P;
		public static readonly Polynomial Zero = new(default(double));
		public double this[double x]
		{
			get
			{
				double r = P[Order];
				for (int i = Order - 1; i >= 0; i--)
					r = r * x + P[i];
				return r;
			}
		}
		public Complex this[Complex x]
		{
			get
			{
				Complex r = new(P[Order]);
				for (int i = Order - 1; i >= 0; i--)
					r = r * x + P[i];
				return r;
			}
		}
		public Polynomial(params double[] p)
		{
			int order = p.Length;
			while (order-- > 1 && p[order] is >= (-1E-10) and <= 1E-10) { }
			Order = order;
			if (order == p.Length - 1)
			{
				P = p;
				return;
			}
			P = new double[order + 1];
			Array.Copy(p, 0, P, 0, P.Length);
		}
		public static Polynomial operator *(Polynomial a, Polynomial b)
		{
			if (a.Order < b.Order)
				return b * a;
			int j = a.Order + b.Order + 1;
			if (b.Order == 0)
				return a * b.P[0];
			if (b.Order == 1)
			{
				double[] P = new double[j];
				double b3 = b.P[1];
				double b2 = b.P[0];
				for (int i = 0; i <= a.Order; i++)
				{
					P[i] += a.P[i] * b2;
					P[i + 1] += a.P[i] * b3;
				}
				return new(P);
			}
			return (a.FFT_2_Time_Position(j) * b.FFT_2_Time_Position(j)).IFFT_2_Time_Position(j);
		}
		public static Polynomial operator *(Polynomial a, double b)
		{
			double[] r = new double[a.Order + 1];
			for (int i = 0; i < r.Length; i++)
				r[i] = a.P[i] * b;
			return new(r);
		}
		public static Polynomial operator *(double b, Polynomial a)
		{
			double[] r = new double[a.Order + 1];
			for (int i = 0; i < r.Length; i++)
				r[i] = a.P[i] * b;
			return new(r);
		}
		public static Polynomial operator +(Polynomial a, Polynomial b)
		{
			if (a.Order < b.Order)
				return b + a;
			double[] r = new double[a.Order + 1];
			Array.Copy(a.P, 0, r, 0, r.Length);
			for (int i = 0; i <= b.Order; i++)
				r[i] += b.P[i];
			return new(r);
		}
		public static Polynomial operator -(Polynomial a, Polynomial b)
		{
			double[] r = new double[(a.Order > b.Order) ? (a.Order + 1) : (b.Order + 1)];
			Array.Copy(a.P, 0, r, 0, a.Order + 1);
			for (int i = 0; i <= b.Order; i++)
				r[i] -= b.P[i];
			return new(r);
		}
		public static Polynomial operator -(Polynomial a)
		{
			double[] r = new double[a.Order + 1];
			for (int i = 0; i < r.Length; i++)
				r[i] = -a.P[i];
			return new(r);
		}
		public Polynomial Mod(Polynomial A)
		{
			if (A.Order == 0)
				return new(default(double));
			double[] a = A.P;
			double pivot = a[A.Order];
			double[] r = new double[A.Order];
			int p = P.Length - a.Length;
			if (p < 0)
				return this;
			Array.Copy(P, p, r, 0, r.Length);
			double t = P[Order] / pivot;
			int k = A.Order - 1;
			while (p-- > 0)
			{
				double temp = (r[k] - t * a[k]) / pivot;
				for (int i = k - 1; i >= 0; i--)
					r[i + 1] = r[i] - t * a[i];
				r[0] = P[p];
				t = temp;
			}
			for (int j = k; j >= 0; j--)
				r[j] -= t * a[j];
			return new(r);
		}
		public Polynomial Divide(Polynomial A, out Polynomial mods)
		{
			if (A.Order == 0)
			{
				mods = new(default(double));
				return this * (1.0 / A.P[0]);
			}
			double[] a = A.P;
			double pivot = a[A.Order];
			double[] r = new double[A.Order];
			int p = P.Length - a.Length;
			if (p < 0)
			{
				mods = this;
				return Zero;
			}
			Array.Copy(P, p, r, 0, r.Length);
			double t = P[Order] / pivot;
			int k = A.Order - 1;
			double[] q = new double[p + 1];
			q[p] = t;
			while (p-- > 0)
			{
				double temp = (r[k] - t * a[k]) / pivot;
				for (int i = k - 1; i >= 0; i--)
					r[i + 1] = r[i] - t * a[i];
				r[0] = P[p];
				t = (q[p] = temp);
			}
			for (int j = k; j >= 0; j--)
				r[j] -= t * a[j];
			mods = new(r);
			return new(q);
		}
        public static Polynomial operator /(Polynomial a, Polynomial b) => a.Divide(b, out _);
        public static Polynomial operator %(Polynomial a, Polynomial b) => a.Mod(b);
        public bool IsZero()
		{
			double[] p2 = P;
			foreach (double p in p2)
				if (p is > 1E-10 or < (-1E-10))
					return false;
			return true;
		}
        public static Polynomial GCD(Polynomial a, Polynomial b) => b.IsZero() ? a : GCD(b, a.Mod(b));
        private void PreFFT(Complex[] complices, Complex[] rs, int index, int step, int length, int offset)
		{
			if (length == 1)
			{
				rs[offset] = complices[index];
				return;
			}
			int nextstep = step << 1;
			int s = length >> 1;
			PreFFT(complices, rs, index, nextstep, s, offset);
			PreFFT(complices, rs, index + step, nextstep, s, offset + s);
		}
		private void FFT_2_Time_Position(Complex[] complices, int index, int length)
		{
			if (length != 1)
			{
				int s = length >> 1;
				FFT_2_Time_Position(complices, index, s);
				FFT_2_Time_Position(complices, index + s, s);
				double phi = Math.PI / (double)s;
				Complex w2 = new(Math.Cos(phi), Math.Sin(phi));
				Complex w = new(1.0);
				for (int i = 0; i < s; i++)
				{
					int m1 = index + i;
					int m2 = m1 + s;
					complices[m2] = complices[m1] - w * complices[m2];
					complices[m1] = complices[m1] * 2 - complices[m2];
					w *= w2;
				}
			}
		}
		public Frequency FFT_2_Time_Position(int L = -1)
		{
			if (L < 0)
				L = Order + 1;
			int length = 1;
			int i;
			for (i = L - 1; i != 0; i >>= 1)
				length <<= 1;
			Complex[] ts = new Complex[length];
			while (i <= Order)
				ts[i] = new(P[i++]);
			while (i < length)
				ts[i++] = new(0.0);
			Complex[] rs = new Complex[length];
			PreFFT(ts, rs, 0, 1, length, 0);
			FFT_2_Time_Position(rs, 0, length);
			return new(rs);
		}
		public Polynomial Sub(int length, int offset = 0)
		{
			double[] r = new double[length];
			Array.Copy(P, offset, r, 0, length);
			return new(r);
		}
		public void Test(double[] xs)
		{
			foreach (double x in xs)
				Console.WriteLine($"{x},{this[x]},{GetDerivativeValue(x)}");
		}
		public override string ToString()
		{
			List<string> s = new();
			for (int i = Order; i > 0; i--)
				if (P[i] is < (-1E-10) or > 1E-10)
					s.Add($"{P[i]}x^{i}");
			if (P[0] is < (-1E-10) or > 1E-10)
				s.Add(P[0].ToString());
            return s.Length == 0 ? "0" : string.Join("+", s);
        }
        public Polynomial GetDerivative()
		{
			double[] r = new double[Order];
			for (int i = Order; i > 0; i--)
				r[i - 1] = i * P[i];
			return new(r);
		}
        public double GetDerivativeValue(double x) => Divide(new(- x, 1.0), out _)[x];
    }
}
