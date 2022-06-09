using System;

namespace TimeSeries;
public class ComplexMorlet 
{
	public int N;
	public int Length;
	public double As;
	public double Wc;
	public double SigmaT;
	public double T;
	public double F;
	public double A;
	public Series Real;
	public Series Imagine;
	public double FWHM => Math.Sqrt(8.0 * Math.Log(2.0)) / SigmaT;
	public ComplexMorlet(double fc, double @as, double t, double f)
	{
		Wc = fc * Math.PI * 2.0;
		N = (int)(t * Wc / Math.Sqrt((0.0 - @as) / 4.34)) + 1;
		SigmaT = N / Wc;
		A = 1.0 / Math.Sqrt(SigmaT * Math.Sqrt(Math.PI));
		F = f;
		T = t;
		Length = (int)(2.0 * t * f) + 1;
		Real = new(Length);
		Imagine = new(Length);
		double num = -2.0 * SigmaT * SigmaT;
		for (int i = 0; i < Length; i++)
		{
			double num2 = i / f - t;
			Real.Values[i] = A * Math.Exp(num2 * num2 / num) * Math.Cos(Wc * num2);
			Imagine.Values[i] = A * Math.Exp(num2 * num2 / num) * Math.Sin(Wc * num2);
		}
	}
	public ComplexMorlet(double fc, double t, int n, double f)
	{
		Wc = fc * Math.PI * 2.0;
		N = n;
		SigmaT = N / Wc;
		A = 1.0 / Math.Sqrt(SigmaT * Math.Sqrt(Math.PI));
		As = -4.34 * (t / SigmaT) * (t / SigmaT);
		F = f;
		T = t;
		Length = (int)(2.0 * t * f) + 1;
		Real = new(Length);
		Imagine = new(Length);
		double num = -2.0 * SigmaT * SigmaT;
		for (int i = 0; i < Length; i++)
		{
			double num2 = i / f - t;
			Real.Values[i] = A * Math.Exp(num2 * num2 / num) * Math.Cos(Wc * num2);
			Imagine.Values[i] = A * Math.Exp(num2 * num2 / num) * Math.Sin(Wc * num2);
		}
	}
	public ComplexSeries GetComplexSeries()
	{
		Complex[] array = new Complex[Length];
		for (int i = 0; i < Length; i++)
		{
			array[i] = new Complex(Real.Values[i], Imagine.Values[i]);
		}
		return new ComplexSeries(array);
	}
}
