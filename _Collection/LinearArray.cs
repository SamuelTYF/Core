using System;

namespace Collection
{
	public class LinearArray<T> where T : IComparable<T>
	{
		public T[] Values;

		public int Length;

		public int Mode;

		public LinearArray(int size, int mode = 1)
		{
			Values = new T[size];
			Mode = mode;
		}

		public int Predecessor(T value, int l, int r)
		{
			if (l == r)
			{
				return l;
			}
			int num = l + r >> 1;
			int num2 = value.CompareTo(Values[num]);
			if (num2 == 0)
			{
				return num;
			}
			if (num2 == Mode)
			{
				return Predecessor(value, num + 1, r);
			}
			return Predecessor(value, l, num);
		}

		public void Insert(T value)
		{
			if (Length == Values.Length)
			{
				throw new Exception();
			}
			int num = Predecessor(value, 0, Length);
			for (int num2 = Length; num2 > num; num2--)
			{
				Values[num2] = Values[num2 - 1];
			}
			Length++;
			Values[num] = value;
		}

		public void Remove(int length, int offset = 0)
		{
			if (length + offset > Length)
			{
				throw new Exception();
			}
			int i = 0;
			int num = offset;
			int num2 = offset + length;
			for (; i < length; i++)
			{
				Values[num++] = Values[num2++];
			}
			Length -= length;
		}

		public override string ToString()
		{
			if (Length == 0)
			{
				return "[]";
			}
			string text = $"[{Values[0]}";
			for (int i = 1; i < Length; i++)
			{
				text += $",{Values[i]}";
			}
			return text + "]";
		}
	}
}
