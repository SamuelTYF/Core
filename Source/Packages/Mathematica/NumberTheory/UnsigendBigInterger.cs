using System;

namespace NumberTheory;

public class UnsigendBigInterger : IComparable<UnsigendBigInterger>
{
	public int[] Values;

	public const int Max = 10000;

	public static readonly Random _R = new(DateTime.Now.Millisecond);

	public UnsigendBigInterger(int n)
	{
		if (n < 0)
			throw new Exception();
		if (n >= 10000)
			Values = new int[2]
			{
				n % 10000,
				n / 10000
			};
		else
			Values = new int[1] { n };
	}

	public UnsigendBigInterger(params int[] n)
	{
		Values = n;
	}

	public UnsigendBigInterger(string s)
	{
		Values = new int[(s.Length - 1) / 4 + 1];
		int num = 0;
		int num2 = Values.Length;
		int num3 = 0;
		char c = s[num3++];
		if (c < '0' || c > '9')
			throw new Exception();
		num = c - 48;
		while ((s.Length - num3) % 4 != 0)
		{
			c = s[num3++];
			if (c < '0' || c > '9')
				throw new Exception();
			num = num * 10 + c - 48;
		}
		Values[--num2] = num;
		num = 0;
		while (num2 != 0)
		{
			c = s[num3++];
			if (c < '0' || c > '9')
				throw new Exception();
			num = num * 10 + c - 48;
			if ((s.Length - num3) % 4 == 0)
			{
				Values[--num2] = num;
				num = 0;
			}
		}
	}

	public static UnsigendBigInterger CreateRandom(int count)
	{
		int[] array = new int[count];
		for (int i = 0; i < count - 1; i++)
		{
			array[i] = _R.Next(10000);
		}
		array[count - 1] = 1 + _R.Next(9999);
		return new UnsigendBigInterger(array);
	}

	public static bool operator <(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		int num2 = values2.Length;
		if (num < num2)
			return true;
		if (num > num2)
			return false;
		for (int num3 = num - 1; num3 >= 0; num3--)
		{
			if (values[num3] > values2[num3])
				return false;
			if (values[num3] < values2[num3])
				return true;
		}
		return false;
	}

	public static bool operator >(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		int num2 = values2.Length;
		if (num < num2)
			return false;
		if (num > num2)
			return true;
		for (int i = num - 1; i >= 0; i++)
		{
			if (values[i] > values2[i])
				return true;
			if (values[i] < values2[i])
				return false;
		}
		return false;
	}

	public static bool operator ==(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		if (num != values2.Length)
			return false;
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			if (values[num2] != values2[num2])
				return false;
		}
		return true;
	}

	public static bool operator !=(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		if (num != values2.Length)
			return true;
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			if (values[num2] != values2[num2])
				return true;
		}
		return false;
	}

	public int CompareTo(UnsigendBigInterger other)
	{
		int[] values = Values;
		int[] values2 = other.Values;
		int num = values.Length;
		int num2 = num.CompareTo(values2.Length);
		if (num2 != 0)
			return num2;
		for (int num3 = num - 1; num3 >= 0; num3--)
		{
			num2 = values[num3].CompareTo(values2[num3]);
			if (num2 != 0)
				return num2;
		}
		return 0;
	}

	public static implicit operator UnsigendBigInterger(int a)
	{
		return new UnsigendBigInterger(a);
	}

	public static UnsigendBigInterger operator +(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		int num2 = values2.Length;
		if (num < num2)
			return b + a;
		List<int> list = new List<int>();
		int num3 = 0;
		int num4 = 0;
		while (num4 < num2)
		{
			num3 = num3 + values[num4] + values2[num4++];
			list.Add(num3 % 10000);
			num3 /= 10000;
		}
		while (num4 < num)
		{
			num3 += values[num4++];
			list.Add(num3 % 10000);
			num3 /= 10000;
		}
		list.Add(num3);
		while (list.Count > 1 && list[list.Length - 1] == 0)
		{
			list.Pop();
		}
		return new UnsigendBigInterger(list.ToArray());
	}

	public static UnsigendBigInterger operator -(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int[] values2 = b.Values;
		int num = values.Length;
		int num2 = values2.Length;
		if (num < num2)
			throw new Exception();
		List<int> list = new List<int>();
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		while (num4 < num2)
		{
			num5 = values[num4] - values2[num4++] + num3;
			if (num5 < 0)
			{
				num5 += 10000;
				num3 = -1;
			}
			else
				num3 = 0;
			list.Add(num5);
		}
		while (num4 < num)
		{
			num5 = values[num4++] + num3;
			if (num5 < 0)
			{
				num5 += 10000;
				num3 = -1;
			}
			else
				num3 = 0;
			list.Add(num5);
		}
		if (num3 == -1)
			throw new Exception();
		while (list.Length > 1 && list[list.Length - 1] == 0)
		{
			list.Pop();
		}
		return new UnsigendBigInterger(list.ToArray());
	}

	public static UnsigendBigInterger operator *(UnsigendBigInterger a, int b)
	{
		int[] values = a.Values;
		int num = values.Length;
		List<int> list = new List<int>();
		int num2 = 0;
		int num3 = 0;
		while (num3 < num)
		{
			num2 += values[num3++] * b;
			list.Add(num2 % 10000);
			num2 /= 10000;
		}
		list.Add(num2);
		while (list.Length > 1 && list[list.Length - 1] == 0)
		{
			list.Pop();
		}
		return new UnsigendBigInterger(list.ToArray());
	}

	public static UnsigendBigInterger operator <<(UnsigendBigInterger a, int b)
	{
		if (a.Values.Length == 1 && a.Values[0] == 0)
			return a;
		int[] array = new int[a.Values.Length + b];
		Array.Copy(a.Values, 0, array, b, a.Values.Length);
		return new UnsigendBigInterger(array);
	}

	public static UnsigendBigInterger operator >>(UnsigendBigInterger a, int b)
	{
		if (a.Values.Length <= b)
			return 0;
		int[] array = new int[a.Values.Length - b];
		Array.Copy(a.Values, b, array, 0, array.Length);
		return new UnsigendBigInterger(array);
	}

	public static UnsigendBigInterger operator *(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		int[] values = a.Values;
		int num = values.Length;
		UnsigendBigInterger result = b * values[0];
		for (int i = 1; i < num; i++)
		{
			result += b * values[i] << i;
		}
		return result;
	}

	public static UnsigendBigInterger operator /(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		Divide(a, b, out var m, out var _);
		return m;
	}

	public static UnsigendBigInterger operator %(UnsigendBigInterger a, UnsigendBigInterger b)
	{
		Divide(a, b, out var _, out var n);
		return n;
	}

	public static int Divide(UnsigendBigInterger a, UnsigendBigInterger b, UnsigendBigInterger[] lists, out UnsigendBigInterger n)
	{
		if (a < b)
		{
			n = a;
			return 0;
		}
		int num = 0;
		int num2 = 10000;
		while (num < num2 - 1)
		{
			int num3 = num + num2 >> 1;
			switch (a.CompareTo(lists[num3] ?? (lists[num3] = b * num3)))
			{
				case 0:
					n = 0;
					return num3;
				case 1:
					num = num3;
					break;
				default:
					num2 = num3;
					break;
			}
		}
		n = a - b * num;
		return num;
	}

	public static void Divide(UnsigendBigInterger a, UnsigendBigInterger b, out UnsigendBigInterger m, out UnsigendBigInterger n)
	{
		UnsigendBigInterger[] lists = new UnsigendBigInterger[10001];
		int[] array = new int[a.Values.Length];
		UnsigendBigInterger n2 = new UnsigendBigInterger(a.Values[array.Length - 1]);
		array[array.Length - 1] = Divide(n2, b, lists, out n2);
		for (int num = array.Length - 2; num >= 0; num--)
		{
			n2 = (n2 << 1) + a.Values[num];
			array[num] = Divide(n2, b, lists, out n2);
		}
		List<int> list = new List<int>(array);
		while (list.Length > 1 && list[list.Length - 1] == 0)
		{
			list.Pop();
		}
		m = new UnsigendBigInterger(list.ToArray());
		n = n2;
	}

	public override string ToString()
	{
		string text = Values[Values.Length - 1].ToString();
		for (int num = Values.Length - 2; num >= 0; num--)
		{
			text += Values[num].ToString().PadLeft(4, '0');
		}
		return text;
	}

	public override bool Equals(object obj)
	{
		if (obj is UnsigendBigInterger)
			return this == obj as UnsigendBigInterger;
		return false;
	}
	public static UnsigendBigInterger GCD(UnsigendBigInterger x, UnsigendBigInterger b)
	{
		if (b == 0)
			return x;
		Divide(x, b, out var _, out var n);
		return GCD(b, n);
	}
}
