using System;
namespace TimeSeries;
public class BigInterger : IComparable<BigInterger>
{
	public bool HasSign;
	public UnsigendBigInterger Value;
	public BigInterger(bool hassign, UnsigendBigInterger value)
	{
		HasSign = !(value == 0) && hassign;
		Value = value;
	}
	public BigInterger(bool hassign, int n)
		: this(n != 0 && hassign, new UnsigendBigInterger(n))
	{
	}
	public BigInterger(int n)
		: this(n < 0, (n < 0) ? (-n) : n)
	{
	}
	public BigInterger(string s)
	{
		if (s[0] == '-')
		{
			HasSign = true;
			Value = new UnsigendBigInterger(s[1..]);
		}
		else
		{
			HasSign = false;
			Value = new UnsigendBigInterger(s);
		}
	}
	public static bool operator <(BigInterger a, BigInterger b)
	{
		if (a.HasSign != b.HasSign)
			return a.HasSign;
		return a.HasSign ^ (a.Value < b.Value);
	}
	public static bool operator >(BigInterger a, BigInterger b)
	{
		if (a.HasSign != b.HasSign)
			return b.HasSign;
		return a.HasSign ^ (a.Value > b.Value);
	}
	public static bool operator ==(BigInterger a, BigInterger b)
	{
		return a.HasSign == b.HasSign && a.Value == b.Value;
	}
	public static bool operator !=(BigInterger a, BigInterger b)
	{
		return a.HasSign != b.HasSign || a.Value != b.Value;
	}
	public int CompareTo(BigInterger other)
	{
		if (HasSign != other.HasSign)
			return (!HasSign) ? 1 : (-1);
		return HasSign ? (-Value.CompareTo(other.Value)) : Value.CompareTo(other.Value);
	}
	public static implicit operator BigInterger(int a)
	{
		return new BigInterger(a);
	}
	public static BigInterger operator +(BigInterger a, BigInterger b)
	{
		if (a.HasSign == b.HasSign)
			return new BigInterger(a.HasSign, a.Value + b.Value);
		if (a.Value < b.Value)
			return new BigInterger(!a.HasSign, b.Value - a.Value);
		return new BigInterger(a.HasSign, a.Value - b.Value);
	}
	public static BigInterger operator -(BigInterger a, BigInterger b)
	{
		if (a.HasSign == b.HasSign)
		{
			if (a.Value < b.Value)
				return new BigInterger(!a.HasSign, b.Value - a.Value);
			return new BigInterger(a.HasSign, a.Value - b.Value);
		}
		return new BigInterger(a.HasSign, a.Value + b.Value);
	}
	public static BigInterger operator <<(BigInterger a, int b)
	{
		return new BigInterger(a.HasSign, a.Value << b);
	}
	public static BigInterger operator *(BigInterger a, BigInterger b)
	{
		return new BigInterger(a.HasSign != b.HasSign, a.Value * b.Value);
	}
	public static void Divide(BigInterger a, BigInterger b, out BigInterger m, out BigInterger n)
	{
		bool hassign = a.HasSign != b.HasSign;
		UnsigendBigInterger.Divide(a.Value, b.Value, out var m2, out var n2);
		m = new BigInterger(hassign, m2);
		n = new BigInterger(hassign, n2);
	}
	public static UnsigendBigInterger GCD(BigInterger a, BigInterger b)
	{
		return UnsigendBigInterger.GCD(a.Value, b.Value);
	}
	public override bool Equals(object obj)
	{
		return obj is BigInterger && this == obj as BigInterger;
	}
	public override int GetHashCode()
	{
		return Value.GetHashCode() + HasSign.GetHashCode();
	}
	public override string ToString()
	{
		return HasSign ? ("-" + Value.ToString()) : Value.ToString();
	}
}
