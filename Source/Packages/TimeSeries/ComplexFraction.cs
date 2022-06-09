using System;

namespace TimeSeries;
public class ComplexFraction
{
	public Fraction R;
	public Fraction I;
	public static readonly ComplexFraction One = new(1, 0);
	public ComplexFraction(Fraction r, Fraction i)
	{
		R = r;
		I = i;
	}
	public static ComplexFraction operator *(ComplexFraction a, ComplexFraction b)
	{
		return new ComplexFraction(a.R * b.R - a.I * b.I, a.R * b.I + a.I * b.R);
	}
	public static ComplexFraction operator *(ComplexFraction a, Fraction b)
	{
		return new ComplexFraction(a.R * b, a.I * b);
	}
	public ComplexFraction Pow()
	{
		return this * this;
	}
	public static ComplexFraction operator -(ComplexFraction a)
	{
		return new ComplexFraction(-a.R, -a.I);
	}
	public ComplexFraction Conjugate()
	{
		return new ComplexFraction(R, -I);
	}
	public static ComplexFraction operator /(ComplexFraction a, Fraction b)
	{
		return new ComplexFraction(a.R / b, a.I / b);
	}
	public static ComplexFraction operator /(ComplexFraction a, ComplexFraction b)
	{
		Fraction fraction = b.Norm2();
		return new ComplexFraction((a.R * b.R + a.I * b.I) / fraction, (a.I * b.R - a.R * b.I) / fraction);
	}
	public static ComplexFraction operator +(ComplexFraction a, ComplexFraction b)
	{
		return new ComplexFraction(a.R + b.R, a.I + b.I);
	}
	public static ComplexFraction operator -(ComplexFraction a, ComplexFraction b)
	{
		return new ComplexFraction(a.R - b.R, a.I - b.I);
	}
	public Fraction Norm2()
	{
		return R * R + I * I;
	}
	public Fraction Norm()
	{
		return Fraction.Sqrt(R * R + I * I);
	}
	public ComplexFraction Sqrt()
	{
		Fraction fraction = Norm();
		return new ComplexFraction(Fraction.Sqrt((fraction + R) / 2), I.CompareTo(0) * Fraction.Sqrt((fraction - R) / 2));
	}
	public override string ToString()
	{
		if (R == 0)
			return $"{I}i";
		switch (I.CompareTo(0))
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
