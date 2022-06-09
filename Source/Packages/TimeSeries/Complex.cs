
using System;
namespace TimeSeries;
public class Complex
{
	public double R;
	public double I;
	public static readonly Complex One = new Complex(1.0);
	public static readonly Complex i = new Complex(0.0, 1.0);
	public Complex(double r, double i = 0.0)
	{
		R = r;
		I = i;
	}
	public static Complex operator *(Complex a, Complex b)
	{
		return new Complex(a.R * b.R - a.I * b.I, a.R * b.I + a.I * b.R);
	}
	public static Complex operator *(Complex a, double b)
	{
		return new Complex(a.R * b, a.I * b);
	}
	public Complex Pow()
	{
		return this * this;
	}
	public static Complex operator -(Complex a)
	{
		return new Complex(0.0 - a.R, 0.0 - a.I);
	}
	public Complex Conjugate()
	{
		return new Complex(R, 0.0 - I);
	}
	public static Complex operator /(Complex a, double b)
	{
		return new Complex(a.R / b, a.I / b);
	}
	public static Complex operator /(Complex a, Complex b)
	{
		double num = b.Norm2();
		return new Complex((a.R * b.R + a.I * b.I) / num, (a.I * b.R - a.R * b.I) / num);
	}
	public static Complex operator +(Complex a, Complex b)
	{
		return new Complex(a.R + b.R, a.I + b.I);
	}
	public static Complex operator -(Complex a, Complex b)
	{
		return new Complex(a.R - b.R, a.I - b.I);
	}
	public double Norm2()
	{
		return R * R + I * I;
	}
	public double Norm()
	{
		return Math.Sqrt(R * R + I * I);
	}
	public double Phi()
	{
		return Math.Atan2(I, R);
	}
	public Complex Sqrt()
	{
		double num = Norm();
		return new Complex(Math.Sqrt((num + R) / 2.0), I.CompareTo(0.0) * Math.Sqrt((num - R) / 2.0));
	}
	public override string ToString()
	{
		if (R == 0.0)
			return $"{I}i";
		switch (I.CompareTo(0.0))
		{
		case 0:
			return R.ToString();
		case 1:
			return $"{R}+{I}i";
		case -1:
			return $"{R}{I}i";
		default:
			throw new Exception();
		}
	}
}
