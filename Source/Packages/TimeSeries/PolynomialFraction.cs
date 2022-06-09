using System;
namespace TimeSeries;
public class PolynomialFraction
{
	public int N;
	public Fraction[] A;
	public Fraction this[Fraction x]
	{
		get
		{
			Fraction fraction = A[0];
			for (int i = 1; i <= N; i++)
			{
				fraction = fraction * x + A[i];
			}
			return fraction;
		}
	}
	public PolynomialFraction(params Fraction[] a)
	{
		N = (A = a).Length - 1;
	}
	public static PolynomialFraction operator *(PolynomialFraction a, PolynomialFraction b)
	{
		if (b.N != 1)
			throw new Exception();
		Fraction[] array = new Fraction[a.N + 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
		}
		Fraction fraction = b.A[1];
		Fraction fraction2 = b.A[0];
		for (int j = 0; j <= a.N; j++)
		{
			array[j] += a.A[j] * fraction2;
			array[j + 1] += a.A[j] * fraction;
		}
		return new PolynomialFraction(array);
	}
	public static PolynomialFraction operator -(PolynomialFraction a, PolynomialFraction b)
	{
		Fraction[] array = new Fraction[a.N + 1];
		Array.Copy(a.A, 0, array, 0, array.Length);
		for (int i = 0; i <= b.N; i++)
		{
			array[i] -= b.A[i];
		}
		return new PolynomialFraction(array);
	}
	public static PolynomialFraction Times(PolynomialFraction a, Fraction u, Fraction v)
	{
		Fraction[] array = new Fraction[a.N + 3];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
		}
		for (int j = 0; j <= a.N; j++)
		{
			array[j] += a.A[j] * v;
			array[j + 1] += a.A[j] * u;
			array[j + 2] += a.A[j];
		}
		return new PolynomialFraction(array);
	}
	public static PolynomialFraction operator *(PolynomialFraction a, Fraction b)
	{
		Fraction[] array = new Fraction[a.N + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = a.A[i] * b;
		}
		return new PolynomialFraction(array);
	}
	public static PolynomialFraction operator +(PolynomialFraction a, PolynomialFraction b)
	{
		if (a.N < b.N)
			return b + a;
		Fraction[] array = new Fraction[a.N + 1];
		Array.Copy(a.A, 0, array, 0, array.Length);
		for (int i = 0; i <= b.N; i++)
		{
			array[i] += b.A[i];
		}
		return new PolynomialFraction(array);
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
