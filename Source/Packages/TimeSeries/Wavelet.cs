using System;
namespace TimeSeries;
public class Wavelet
{
	public int Length;
	public double[] H;
	public double[] G;
	public int Level;
	public Series[] Data;
	public static readonly double[] Haar = new double[2]
	{
		1.0 / Math.Sqrt(2.0),
		1.0 / Math.Sqrt(2.0)
	};
	public static readonly double[] D4 = new double[4]
	{
		(1.0 - Math.Sqrt(3.0)) / Math.Sqrt(32.0),
		(3.0 - Math.Sqrt(3.0)) / Math.Sqrt(32.0),
		(3.0 + Math.Sqrt(3.0)) / Math.Sqrt(32.0),
		(1.0 + Math.Sqrt(3.0)) / Math.Sqrt(32.0)
	};
	public void DWT(Series source, out Series a, out Series b)
	{
		if (source.Length < Length)
			throw new Exception();
		int num = source.Length >> 1;
		if ((source.Length & 1) == 1)
		{
			a = new(num + 1);
			b = new(num + 1);
		}
		else
		{
			a = new(num);
			b = new(num);
		}
		int length = source.Length;
		int num2 = Length >> 1;
		int[] array = new int[num2];
		for (int i = 0; i < num2; i++)
		{
			array[i] = i - (num2 - 1);
		}
		for (int j = 0; j < length; j++)
		{
			int num3 = j >> 1;
			int num4 = (j + 1) & 1;
			for (int k = 0; k < num2; k++)
			{
				int num5 = array[k] + num3;
				if (num5 < 0)
					num5 += num;
				a.Values[num5] += source.Values[j] * H[(k << 1) | num4];
				b.Values[num5] += source.Values[j] * G[(k << 1) | num4];
			}
		}
	}
	public Series IDWF(Series a, Series b)
	{
		int length = b.Length;
		int num = length << 1;
		Series series = new(num);
		int num2 = Length >> 1;
		int[] array = new int[num2];
		for (int i = 0; i < num2; i++)
		{
			array[i] = i - (num2 - 1);
		}
		for (int j = 0; j < num; j++)
		{
			int num3 = j >> 1;
			int num4 = (j + 1) & 1;
			for (int k = 0; k < num2; k++)
			{
				int num5 = array[k] + num3;
				if (num5 < 0)
					num5 += length;
				series.Values[j] += a.Values[num5] * H[(k << 1) | num4] + b.Values[num5] * G[(k << 1) | num4];
			}
		}
		return series;
	}
	public Wavelet(Series source, int level, double[] h)
	{
		H = h;
		G = new double[Length = h.Length];
		int num = 0;
		int num2 = Length;
		while (num < Length)
		{
			G[num++] = 0.0 - h[--num2];
			G[num++] = h[--num2];
		}
		Level = level;
		Data = new Series[Level + 1];
		Series series = source;
		for (int i = 1; i <= level; i++)
		{
			DWT(series, out var a, out var b);
			Data[i] = b;
			series = a;
		}
		Data[0] = series;
	}
	public Series GetSeries()
	{
		Series series = Data[0];
		for (int num = Level; num > 0; num--)
		{
			series = IDWF(series, Data[num]);
		}
		return series;
	}
	public static double[] Daubechies(int N, double error = 0.0001, double max = 10000.0)
	{
		double[] array = new double[N];
		array[0] = 1.0;
		for (int i = 1; i < N; i++)
		{
			array[i] = array[i - 1] * (N + i - 1) / (2 * i);
		}
		for (int j = 0; j < N; j++)
		{
			array[j] /= array[N - 1];
		}
		Complex[] roots = FactorHelper.GetRoots(array, error, max);
		Complex[] array2 = new Complex[N - 1];
		for (int k = 0; k < N - 1; k++)
		{
			Complex complex = Complex.One - roots[k];
			Complex complex2 = complex - (complex.Pow() - Complex.One).Sqrt();
			if (complex2.Norm2() > 1.0)
				throw new Exception();
			array2[k] = complex2;
		}
		Polynomial realPolynomial = PolynomialComplex.FromRoots(array2).GetRealPolynomial();
		Polynomial polynomial = realPolynomial * (Math.Sqrt(2.0) / ((1 << N) * realPolynomial[1.0]));
		Polynomial polynomial2 = new(1.0, 1.0);
		for (int l = 0; l < N; l++)
		{
			polynomial *= polynomial2;
		}
		return polynomial.A;
	}
	public static Complex[] DaubechiesComplex(int N, double error = 0.0001)
	{
		double[] array = new double[N];
		array[0] = 1.0;
		for (int i = 1; i < N; i++)
		{
			array[i] = array[i - 1] * (N + i - 1) / (2 * i);
		}
		Complex[] roots = FactorHelper.GetRoots(array, error);
		Complex[] array2 = new Complex[N - 1];
		for (int j = 0; j < N - 1; j++)
		{
			Complex complex = Complex.One - roots[j];
			Complex complex2 = complex - (complex.Pow() - Complex.One).Sqrt();
			if (complex2.Norm2() > 1.0)
				throw new Exception();
			array2[j] = complex2;
		}
		PolynomialComplex polynomialComplex = PolynomialComplex.FromRoots(array2);
		PolynomialComplex polynomialComplex2 = polynomialComplex * (new Complex(Math.Sqrt(2.0)) / (polynomialComplex[Complex.One] * (1 << N)));
		PolynomialComplex polynomialComplex3 = new(Complex.One, Complex.One);
		for (int k = 0; k < N; k++)
		{
			polynomialComplex2 *= polynomialComplex3;
		}
		return polynomialComplex2.A;
	}
	public static double[] DaubechiesFraction(int N, Fraction error)
	{
		Fraction[] array = new Fraction[N];
		array[0] = 1;
		for (int i = 1; i < N; i++)
		{
			array[i] = array[i - 1] * (N + i - 1) / (2 * i);
		}
		for (int j = 0; j < N; j++)
		{
			array[j] /= array[N - 1];
		}
		ComplexFraction[] roots = FactorHelperFraction.GetRoots(array, error);
		ComplexFraction[] array2 = new ComplexFraction[N - 1];
		for (int k = 0; k < N - 1; k++)
		{
			ComplexFraction complexFraction = ComplexFraction.One - roots[k];
			ComplexFraction complexFraction2 = complexFraction - (complexFraction.Pow() - ComplexFraction.One).Sqrt();
			if (complexFraction2.Norm2() > 1)
				throw new Exception();
			array2[k] = complexFraction2;
		}
		PolynomialComplexFraction polynomialComplexFraction2 = PolynomialComplexFraction.FromRoots(array2);
		PolynomialComplexFraction polynomialComplexFraction3 = polynomialComplexFraction2 * (new ComplexFraction(2, 0).Sqrt() / (new ComplexFraction(1 << N, 0) * polynomialComplexFraction2[ComplexFraction.One]));
		PolynomialComplexFraction polynomialComplexFraction4 = new(ComplexFraction.One, ComplexFraction.One);
		for (int l = 0; l < N; l++)
		{
			polynomialComplexFraction3 *= polynomialComplexFraction4;
		}
		double[] array3 = new double[polynomialComplexFraction3.N + 1];
		for (int m = 0; m < array3.Length; m++)
		{
			array3[m] = polynomialComplexFraction3.A[m].R.ToDouble();
		}
		return array3;
	}
}
