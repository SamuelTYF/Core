using System;
using System.Collections.Generic;
namespace TimeSeries;
public class PolynomialComplex
{
	public int N;
	public Complex[] A;
	public Complex this[Complex x]
	{
		get
		{
			Complex complex = A[0];
			for (int i = 1; i <= N; i++)
			{
				complex = complex * x + A[i];
			}
			return complex;
		}
	}
	public PolynomialComplex(params Complex[] a)
	{
		N = (A = a).Length - 1;
	}
	public static PolynomialComplex operator *(PolynomialComplex a, PolynomialComplex b)
	{
		if (b.N != 1)
			throw new Exception();
		Complex[] array = new Complex[a.N + 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new Complex(0.0);
		}
		Complex complex = b.A[1];
		Complex complex2 = b.A[0];
		for (int j = 0; j <= a.N; j++)
		{
			array[j] += a.A[j] * complex2;
			array[j + 1] += a.A[j] * complex;
		}
		return new PolynomialComplex(array);
	}
	public static PolynomialComplex operator *(PolynomialComplex a, Complex b)
	{
		Complex[] array = new Complex[a.N + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = a.A[i] * b;
		}
		return new PolynomialComplex(array);
	}
	public Polynomial GetRealPolynomial()
	{
		double[] array = new double[A.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (A[i].R >= 0.0)
				array[i] = A[i].Norm();
			else
				array[i] = 0.0 - A[i].Norm();
		}
		return new Polynomial(array);
	}
	public static PolynomialComplex FromRoots(IEnumerable<Complex> roots)
	{
		PolynomialComplex result = new PolynomialComplex(Complex.One);
		foreach (Complex root in roots)
		{
			result *= new PolynomialComplex(-root, Complex.One);
		}
		return result;
	}
	public override string ToString()
	{
		string text = "";
		for (int num = N; num > 0; num--)
		{
			text += $"{A[num]}x^{num}+";
		}
		return text + A[0].ToString();
	}
}
