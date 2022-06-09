using System;
namespace TimeSeries;
public class Polynomial
{
	public int N;
	public double[] A;
	public double this[double x]
	{
		get
		{
			double num = A[0];
			for (int i = 1; i <= N; i++)
			{
				num = num * x + A[i];
			}
			return num;
		}
	}
	public Polynomial(params double[] a)
	{
		N = (A = a).Length - 1;
	}
	public static Polynomial operator *(Polynomial a, Polynomial b)
	{
		if (b.N != 1)
			throw new Exception();
		double[] array = new double[a.N + 2];
		double num = b.A[1];
		double num2 = b.A[0];
		for (int i = 0; i <= a.N; i++)
		{
			array[i] += a.A[i] * num2;
			array[i + 1] += a.A[i] * num;
		}
		return new Polynomial(array);
	}
	public static Polynomial operator *(Polynomial a, double b)
	{
		double[] array = new double[a.N + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = a.A[i] * b;
		}
		return new Polynomial(array);
	}
	public static Polynomial operator +(Polynomial a, Polynomial b)
	{
		if (a.N < b.N)
			return b + a;
		double[] array = new double[a.N + 1];
		Array.Copy(a.A, 0, array, 0, array.Length);
		for (int i = 0; i <= b.N; i++)
		{
			array[i] += b.A[i];
		}
		return new Polynomial(array);
	}
	public override string ToString()
	{
		string text = "";
		for (int num = N; num > 0; num--)
		{
			text += $"{A[num]}x^{num}+";
		}
		return text + A[0];
	}
}
