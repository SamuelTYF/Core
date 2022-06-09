using System;
namespace TimeSeries;
public class Fraction : IComparable<Fraction>
{
	public BigInterger P;
	public BigInterger Q;
	public static int L = 20;
	public static Fraction Error = new Fraction(1, 100000000);
	public Fraction(BigInterger p, BigInterger q = null)
	{
		q = q ?? ((BigInterger)1);
		if (p.Value.Values.Length > L && q.Value.Values.Length > L)
		{
			int num = p.Value.Values.Length - L;
			int num2 = q.Value.Values.Length - L;
			int num3 = ((num < num2) ? num : num2);
			p = new BigInterger(p.HasSign, p.Value >> num3);
			q = new BigInterger(q.HasSign, q.Value >> num3);
		}
		UnsigendBigInterger b = BigInterger.GCD(p, q);
		UnsigendBigInterger.Divide(p.Value, b, out var m, out var n);
		UnsigendBigInterger.Divide(q.Value, b, out var m2, out n);
		P = new BigInterger(p.HasSign != q.HasSign, m);
		Q = new BigInterger(hassign: false, m2);
	}
	public static bool operator <(Fraction a, Fraction b)
	{
		return a.P * b.Q < a.Q * b.P;
	}
	public static bool operator >(Fraction a, Fraction b)
	{
		return a.P * b.Q > a.Q * b.P;
	}
	public static bool operator ==(Fraction a, Fraction b)
	{
		return a.P * b.Q == a.Q * b.P;
	}
	public static bool operator !=(Fraction a, Fraction b)
	{
		return a.P * b.Q != a.Q * b.P;
	}
	public static Fraction operator +(Fraction a, Fraction b)
	{
		return new Fraction(a.P * b.Q + a.Q * b.P, a.Q * b.Q);
	}
	public static Fraction operator -(Fraction a, Fraction b)
	{
		return new Fraction(a.P * b.Q - a.Q * b.P, a.Q * b.Q);
	}
	public static Fraction operator *(Fraction a, Fraction b)
	{
		return new Fraction(a.P * b.P, a.Q * b.Q);
	}
	public static Fraction operator /(Fraction a, Fraction b)
	{
		return new Fraction(a.P * b.Q, a.Q * b.P);
	}
	public static implicit operator Fraction(int a)
	{
		return new Fraction(a);
	}
	public static Fraction operator -(Fraction a)
	{
		return a * -1;
	}
	public override int GetHashCode()
	{
		return P.GetHashCode() + Q.GetHashCode();
	}
	public override string ToString()
	{
		UnsigendBigInterger.Divide(P.Value << 4, Q.Value, out var m, out var _);
		string text = m.ToString();
		text = ((text.Length > 16) ? (text.Substring(0, text.Length - 16) + "." + text.Substring(text.Length - 16)) : ("0." + text.PadLeft(16, '0')));
		return P.HasSign ? ("-" + text) : text;
	}
	public override bool Equals(object obj)
	{
		return obj is Fraction && this == obj as Fraction;
	}
	public static Fraction Sqrt(Fraction a)
	{
		if (a.P.HasSign)
		{
			if (-a < Error)
				return 0;
			throw new Exception();
		}
		return new Fraction(new BigInterger(hassign: false, UnsigendBigInterger.Sqrt(a.P.Value << 2)), new BigInterger(hassign: false, UnsigendBigInterger.Sqrt(a.Q.Value << 2)));
	}
	public double ToDouble()
	{
		return Convert.ToDouble(ToString());
	}
	public int CompareTo(Fraction other)
	{
		if (this > other)
			return 1;
		if (this < other)
			return -1;
		return 0;
	}
}
