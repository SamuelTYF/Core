using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class Wedge<T> : IEnumerable<T>, IEnumerable where T : IComparable<T>
	{
		public WedgeNode<T> Top;

		public WedgeNode<T> End;

		public int Count;

		public int Mode;

		public Wedge(int mode)
		{
			Count = 0;
			Mode = mode;
		}

		public void Insert(T value, int index)
		{
			while (Count != 0 && End.Value.CompareTo(value).CompareTo(0) != Mode)
			{
				Count--;
				End = End.Next;
			}
			if (Count == 0)
			{
				Top = (End = new WedgeNode<T>(null, value, index));
			}
			else
			{
				End = (End.Last = new WedgeNode<T>(End, value, index));
			}
			Count++;
		}

		public void Pop(int index)
		{
			while (Count != 0 && Top.Index < index)
			{
				Count--;
				Top = Top.Last;
			}
			if (Count == 0)
			{
				End = null;
			}
		}

		public void Reset()
		{
			Count = 0;
			Top = (End = null);
		}

		public override string ToString()
		{
			return string.Join(",", this);
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (WedgeNode<T> i = Top; i != null; i = i.Last)
			{
				yield return i.Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static T[] Filter(T[] values, int window, int mode)
		{
			int num = values.Length;
			T[] array = new T[num];
			Wedge<T> wedge = new Wedge<T>(mode);
			int num2 = 0;
			int num3 = 0;
			while (num2 < window)
			{
				wedge.Insert(values[num2], num2++);
			}
			while (num2 < num)
			{
				array[num3] = wedge.Top.Value;
				wedge.Pop(++num3);
				wedge.Insert(values[num2], num2++);
			}
			while (num3 < num)
			{
				array[num3] = wedge.Top.Value;
				wedge.Pop(++num3);
			}
			return array;
		}

		public static T[] MinimumFilter(T[] values, int w)
		{
			int num = w << 1;
			int num2 = values.Length;
			T[] array = new T[num2];
			Wedge<T> wedge = new Wedge<T>(-1);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			while (num3 < w)
			{
				wedge.Insert(values[num3], num3++);
			}
			while (num3 < num)
			{
				wedge.Insert(values[num3], num3++);
				array[num4++] = wedge.Top.Value;
			}
			while (num3 < num2)
			{
				wedge.Insert(values[num3], num3++);
				array[num4++] = wedge.Top.Value;
				wedge.Pop(num5++);
			}
			while (num4 < num2)
			{
				array[num4++] = wedge.Top.Value;
				wedge.Pop(num5++);
			}
			return array;
		}
	}
}
