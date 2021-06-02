using System;
using Collection.Serialization;
namespace NumericalAnalysis
{
	public class Frequency : ISerializable
	{
		public int Length;
		public Complex[] Values;
        public Frequency(int length) => Values = new Complex[Length = length];
        public Frequency(Complex[] values) => Length = (Values = values).Length;
        public Frequency(Formatter formatter)
			: this(formatter.Read() as Complex[])
		{
		}
        public void Write(Formatter formatter) => formatter.Write(Values);
        private void PreIFFT(Complex[] complices, Complex[] rs, int index, int step, int length, int offset)
		{
			if (length == 1)
			{
				rs[offset] = complices[index];
				return;
			}
			int nextstep = step << 1;
			int s = length >> 1;
			PreIFFT(complices, rs, index, nextstep, s, offset);
			PreIFFT(complices, rs, index + step, nextstep, s, offset + s);
		}
		private void IFFT_2_Time_Position(Complex[] complices, int index, int length)
		{
			if (length != 1)
			{
				int s = length >> 1;
				IFFT_2_Time_Position(complices, index, s);
				IFFT_2_Time_Position(complices, index + s, s);
				double phi = Math.PI / (double)s;
				Complex w2 = new(Math.Cos(phi), - Math.Sin(phi));
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
		private double[] PostIFFT(Complex[] complices)
		{
			int length = complices.Length;
			double[] r = new double[length];
			for (int i = 0; i < length; i++)
				r[i] = complices[i].R.CompareTo(0.0) * (complices[i].Norm() / (double)length);
			return r;
		}
		public Polynomial IFFT_2_Time_Position(int L = -1)
		{
			if (L < 0)
				L = Length;
			int length = 1;
			int i;
			for (i = L - 1; i != 0; i >>= 1)
				length <<= 1;
			Complex[] ts = new Complex[length];
			while (i < Length)
				ts[i] = Values[i++];
			while (i < length)
				ts[i++] = new(0.0);
			Complex[] rs = new Complex[length];
			PreIFFT(ts, rs, 0, 1, length, 0);
			IFFT_2_Time_Position(rs, 0, length);
			return new(PostIFFT(rs));
		}
		public Frequency Sub(int length, int index = 0)
		{
			Frequency r = new(length);
			Array.Copy(Values, index, r.Values, 0, length);
			return r;
		}
		public static Frequency operator *(Frequency a, double b)
		{
			Complex[] r = new Complex[a.Length];
			for (int i = 0; i < a.Length; i++)
				r[i] = a.Values[i] * b;
			return new(r);
		}
		public static Frequency operator +(Frequency a, Frequency b)
		{
			if (a.Length != b.Length)
				throw new Exception();
			Complex[] r = new Complex[a.Length];
			for (int i = 0; i < a.Length; i++)
				r[i] = a.Values[i] + b.Values[i];
			return new(r);
		}
		public static Frequency operator *(Frequency a, Frequency b)
		{
			if (a.Length != b.Length)
				throw new Exception();
			Complex[] r = new Complex[a.Length];
			for (int i = 0; i < a.Length; i++)
				r[i] = a.Values[i] * b.Values[i];
			return new(r);
		}
		public static Frequency operator /(Frequency a, Frequency b)
		{
			if (a.Length != b.Length)
				throw new Exception();
			Complex[] r = new Complex[a.Length];
			for (int i = 0; i < a.Length; i++)
				r[i] = a.Values[i] / b.Values[i];
			return new(r);
		}
		public static int GetLength(int length)
		{
			int i;
			while ((i = length & -length) != length)
				length += i;
			return length;
		}
		public static Frequency Zero(int order)
		{
			int length = GetLength(order);
			Frequency f = new(length);
			for (int i = 0; i < length; i++)
				f.Values[i] = Complex.Zero;
			return f;
		}
		public static Frequency One(int order)
		{
			int length = GetLength(order);
			Frequency f = new(length);
			for (int i = 0; i < length; i++)
				f.Values[i] = Complex.One;
			return f;
		}
        public override string ToString() => "{" + string.Join<Complex>(",",Values) + "}";
    }
}
