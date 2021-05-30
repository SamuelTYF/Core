using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class DiscreteSet<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public T Inf;

		public T Sup;

		public T[] Values;

		public AVL<T, int> Indexes;

		private DiscreteSet(params T[] values)
		{
			Values = values;
			Inf = Values[0];
			Sup = Values[values.Length - 1];
			Indexes = new AVL<T, int>();
			for (int i = 0; i < values.Length; i++)
			{
				Indexes[values[i]] = i;
			}
		}

		public bool Contain(T value)
		{
			return Indexes.ContainsKey(value);
		}

		public bool IsIn(DiscreteSet<T> other)
		{
			if (Values.Length > other.Values.Length)
			{
				return false;
			}
			if (Inf.CompareTo(other.Inf) < 0)
			{
				return false;
			}
			if (Sup.CompareTo(other.Sup) > 0)
			{
				return false;
			}
			T[] values = Values;
			foreach (T value in values)
			{
				if (!other.Contain(value))
				{
					return false;
				}
			}
			return true;
		}

		public ISet<T> GetIntersection(DiscreteSet<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (!Indexes.Findgeq(other.Inf, ref value))
			{
				return new EmptySet<T>();
			}
			if (!Indexes.Findleq(other.Sup, ref value2))
			{
				return new EmptySet<T>();
			}
			int value3 = 0;
			int value4 = other.Values.Length - 1;
			if (!other.Indexes.Findgeq(Inf, ref value3))
			{
				return new EmptySet<T>();
			}
			if (!other.Indexes.Findleq(Sup, ref value4))
			{
				return new EmptySet<T>();
			}
			List<T> list = new List<T>();
			while (value <= value2 && value3 <= value4)
			{
				int num = Values[value].CompareTo(other.Values[value3]);
				if (num > 0)
				{
					value3++;
					continue;
				}
				if (num < 0)
				{
					value++;
					continue;
				}
				list.Add(Values[value]);
				value++;
				value3++;
			}
			if (list.Length == 0)
			{
				return new EmptySet<T>();
			}
			return new DiscreteSet<T>(list.ToArray());
		}

		public ISet<T> GetIntersection(Interval<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (other.DownBlock)
			{
				if (!Indexes.Findgeq(other.Inf, ref value))
				{
					return new EmptySet<T>();
				}
			}
			else if (!Indexes.Successor(other.Inf, ref value))
			{
				return new EmptySet<T>();
			}
			if (other.UpBlock)
			{
				if (!Indexes.Findleq(other.Sup, ref value2))
				{
					return new EmptySet<T>();
				}
			}
			else if (!Indexes.Predecessor(other.Sup, ref value2))
			{
				return new EmptySet<T>();
			}
			if (value > value2)
			{
				return new EmptySet<T>();
			}
			if (value == 0 && value2 == Values.Length - 1)
			{
				return this;
			}
			T[] array = new T[value2 - value + 1];
			Array.Copy(Values, value, array, 0, array.Length);
			return new DiscreteSet<T>(array);
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			if (other is DiscreteSet<T>)
			{
				return GetIntersection(other as DiscreteSet<T>);
			}
			if (other is Interval<T>)
			{
				return GetIntersection(other as Interval<T>);
			}
			throw new NotImplementedException();
		}

		public static ISet<T> CreateFromSortedArray(params T[] values)
		{
			if (values.Length == 0)
			{
				return new EmptySet<T>();
			}
			return new DiscreteSet<T>(values);
		}

		public static ISet<T> CreateFromArray(params T[] values)
		{
			if (values.Length == 0)
			{
				return new EmptySet<T>();
			}
			AVL<T, bool> aVL = new AVL<T, bool>();
			foreach (T key in values)
			{
				aVL[key] = true;
			}
			return new DiscreteSet<T>(aVL.LDROrder());
		}

		public override string ToString()
		{
			return "{" + string.Join(",", Values) + "}";
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			yield return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public DiscreteSet<T> Union(DiscreteSet<T> other)
		{
			AVL<T, bool> aVL = new AVL<T, bool>();
			T[] values = Values;
			foreach (T key in values)
			{
				aVL[key] = true;
			}
			T[] values2 = other.Values;
			foreach (T key2 in values2)
			{
				aVL[key2] = true;
			}
			return new DiscreteSet<T>(aVL.LDROrder());
		}

		public ISet<T> DifferenceSet(Interval<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (other.DownBlock)
			{
				if (!Indexes.Findgeq(other.Inf, ref value))
				{
					return this;
				}
			}
			else if (!Indexes.Successor(other.Inf, ref value))
			{
				return this;
			}
			if (other.UpBlock)
			{
				if (!Indexes.Findleq(other.Sup, ref value2))
				{
					return this;
				}
			}
			else if (!Indexes.Predecessor(other.Sup, ref value2))
			{
				return this;
			}
			if (value > value2)
			{
				return this;
			}
			if (value == 0 && value2 == Values.Length - 1)
			{
				return new EmptySet<T>();
			}
			T[] array = new T[Values.Length - (value2 - value + 1)];
			Array.Copy(Values, 0, array, 0, value);
			Array.Copy(Values, value2 + 1, array, value, Values.Length - value2 - 1);
			return new DiscreteSet<T>(array);
		}

		public ISet<T> DifferenceSet(Intervals<T> other)
		{
			DiscreteSet<T> discreteSet = this;
			Interval<T>[] values = other.Values;
			foreach (Interval<T> other2 in values)
			{
				if ((discreteSet = discreteSet.DifferenceSet(other2) as DiscreteSet<T>) == null)
				{
					return new EmptySet<T>();
				}
			}
			return discreteSet;
		}

		public ISet<T> DifferenceSet(ISet<T> other)
		{
			if (other is Interval<T>)
			{
				return DifferenceSet(other as Interval<T>);
			}
			if (other is Intervals<T>)
			{
				return DifferenceSet(other as Intervals<T>);
			}
			throw new Exception();
		}
	}
}
