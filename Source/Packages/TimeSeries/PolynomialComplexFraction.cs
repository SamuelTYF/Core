using System;
using System.Collections.Generic;
namespace TimeSeries;
public class PolynomialComplexFraction
{
	public int N;
	public ComplexFraction[] A;
	public ComplexFraction this[ComplexFraction x]
	{
		get
		{
			ComplexFraction complexFraction = A[0];
			for (int i = 1; i <= N; i++)
			{
				complexFraction = complexFraction * x + A[i];
			}
			return complexFraction;
		}
	}
	public PolynomialComplexFraction(params ComplexFraction[] a)
	{
		N = (A = a).Length - 1;
	}
	public static PolynomialComplexFraction operator *(PolynomialComplexFraction a, PolynomialComplexFraction b)
	{
		if (b.N != 1)
			throw new Exception();
		ComplexFraction[] array = new ComplexFraction[a.N + 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new ComplexFraction(0, 0);
		}
		ComplexFraction complexFraction = b.A[1];
		ComplexFraction complexFraction2 = b.A[0];
		for (int j = 0; j <= a.N; j++)
		{
			array[j] += a.A[j] * complexFraction2;
			array[j + 1] += a.A[j] * complexFraction;
		}
		return new PolynomialComplexFraction(array);
	}
	public static PolynomialComplexFraction operator *(PolynomialComplexFraction a, ComplexFraction b)
	{
		ComplexFraction[] array = new ComplexFraction[a.N + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = a.A[i] * b;
		}
		return new PolynomialComplexFraction(array);
	}
	public Polynomial GetRealPolynomial()
	{
		double[] array = new double[A.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (A[i].R > 0)
				array[i] = A[i].Norm().ToDouble();
			else
				array[i] = 0.0 - A[i].Norm().ToDouble();
		}
		return new Polynomial(array);
	}
	public static PolynomialComplexFraction FromRoots(IEnumerable<ComplexFraction> roots)
	{
		PolynomialComplexFraction result = new(ComplexFraction.One);
		foreach (ComplexFraction root in roots)
			result *= new PolynomialComplexFraction(-root, ComplexFraction.One);
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
