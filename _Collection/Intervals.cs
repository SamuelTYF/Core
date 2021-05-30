using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class Intervals<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public Interval<T>[] Values;

		public AVL<T, int> Infs;

		public AVL<T, int> Sups;

		private Intervals(params Interval<T>[] values)
		{
			Values = values;
			Infs = new AVL<T, int>();
			Sups = new AVL<T, int>();
			for (int i = 0; i < values.Length; i++)
			{
				Infs[values[i].Inf] = i;
				Sups[values[i].Sup] = i;
			}
		}

		public ISet<T> GetIntersection(DiscreteSet<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (!Infs.Findgeq(other.Sup, ref value2))
			{
				return new EmptySet<T>();
			}
			if (!Sups.Findleq(other.Inf, ref value))
			{
				return new EmptySet<T>();
			}
			List<T> list = new List<T>();
			for (int i = value; i <= value2; i++)
			{
				DiscreteSet<T> discreteSet = other.GetIntersection(Values[i]) as DiscreteSet<T>;
				if (discreteSet != null)
				{
					list.AddRange(discreteSet.Values);
				}
			}
			if (list.Length == 0)
			{
				return new EmptySet<T>();
			}
			return DiscreteSet<T>.CreateFromSortedArray(list.ToArray());
		}

		public ISet<T> GetIntersection(Interval<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (!Infs.Findgeq(other.Sup, ref value2))
			{
				return new EmptySet<T>();
			}
			if (!Sups.Findleq(other.Inf, ref value))
			{
				return new EmptySet<T>();
			}
			if (value == value2)
			{
				return Values[value].GetIntersection(other);
			}
			List<Interval<T>> list = new List<Interval<T>>();
			List<T> list2 = new List<T>();
			ISet<T> intersection = Values[value].GetIntersection(other);
			if (intersection is Interval<T>)
			{
				list.Add(intersection as Interval<T>);
			}
			else if (intersection is DiscreteSet<T>)
			{
				list2.AddRange((intersection as DiscreteSet<T>).Values);
			}
			else if (!(intersection is EmptySet<T>))
			{
				throw new Exception();
			}
			for (int i = value + 1; i < value2; i++)
			{
				list.Add(Values[i]);
			}
			intersection = Values[value2].GetIntersection(other);
			if (intersection is Interval<T>)
			{
				list.Add(intersection as Interval<T>);
			}
			else if (intersection is DiscreteSet<T>)
			{
				list2.AddRange((intersection as DiscreteSet<T>).Values);
			}
			else if (!(intersection is EmptySet<T>))
			{
				throw new Exception();
			}
			if (list.Length == 0)
			{
				return DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
			}
			if (list2.Length == 0)
			{
				if (list.Length == 1)
				{
					return list[0];
				}
				return new Intervals<T>(list.ToArray());
			}
			DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
			if (list.Length == 1)
			{
				return new Set<T>(discrete, list[0]);
			}
			return new Set<T>(discrete, new Intervals<T>(list.ToArray()));
		}

		public ISet<T> GetIntersection(Intervals<T> other)
		{
			List<Interval<T>> list = new List<Interval<T>>();
			List<T> list2 = new List<T>();
			Interval<T>[] values = Values;
			foreach (Interval<T> other2 in values)
			{
				foreach (ISet<T> item in other.GetIntersection(other2))
				{
					if (item is Interval<T>)
					{
						list.Add(item as Interval<T>);
						continue;
					}
					if (item is DiscreteSet<T>)
					{
						list2.AddRange((item as DiscreteSet<T>).Values);
						continue;
					}
					throw new Exception();
				}
			}
			if (list.Length == 0)
			{
				if (list2.Length == 0)
				{
					return new EmptySet<T>();
				}
				return DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
			}
			if (list2.Length == 0)
			{
				if (list.Length == 1)
				{
					return list[0];
				}
				return new Intervals<T>(list.ToArray());
			}
			DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
			if (list.Length == 1)
			{
				return new Set<T>(discrete, list[0]);
			}
			return new Set<T>(discrete, new Intervals<T>(list.ToArray()));
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			Interval<T>[] values = Values;
			for (int i = 0; i < values.Length; i++)
			{
				yield return values[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static ISet<T> CreateFromArray(params Interval<T>[] values)
		{
			AVL<T, Interval<T>> aVL = new AVL<T, Interval<T>>();
			foreach (Interval<T> interval in values)
			{
				T inf = interval.Inf;
				if (aVL.ContainsKey(inf))
				{
					aVL[inf] = interval.GetUnion(aVL[inf], out var _) as Interval<T>;
				}
				else
				{
					aVL[inf] = interval;
				}
			}
			if (aVL.Length == 0)
			{
				return new EmptySet<T>();
			}
			if (aVL.Length == 1)
			{
				return aVL.GetFirstValue();
			}
			Interval<T>[] array = aVL.LDRSort();
			Interval<T> interval2 = array[0];
			List<Interval<T>> list = new List<Interval<T>>();
			for (int j = 1; j < array.Length; j++)
			{
				bool adjacent2;
				Interval<T> interval3 = interval2.GetUnion(array[j], out adjacent2) as Interval<T>;
				if (adjacent2)
				{
					interval2 = interval3;
					continue;
				}
				list.Add(interval2);
				interval2 = interval3;
			}
			list.Add(interval2);
			return new Intervals<T>(list.ToArray());
		}

		public override string ToString()
		{
			return string.Join("+", (IEnumerable<Interval<T>>)Values);
		}

		public bool Contain(T value)
		{
			int value2 = 0;
			int value3 = Values.Length - 1;
			if (!Infs.Findleq(value, ref value2))
			{
				return false;
			}
			if (!Sups.Findgeq(value, ref value3))
			{
				return false;
			}
			while (value2 <= value3)
			{
				if (Values[value2++].Contain(value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
