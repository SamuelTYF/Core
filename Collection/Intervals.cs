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
			int inf = 0;
			int sup = Values.Length - 1;
			if (!Infs.Findgeq(other.Sup, ref sup))
				return new EmptySet<T>();
			if (!Sups.Findleq(other.Inf, ref inf))
				return new EmptySet<T>();
			List<T> r = new List<T>();
			for (int i = inf; i <= sup; i++)
			{
				DiscreteSet<T> discreteSet = other.GetIntersection(Values[i]) as DiscreteSet<T>;
				if (discreteSet != null)
					r.AddRange(discreteSet.Values);
			}
			if (r.Length == 0)
				return new EmptySet<T>();
			return DiscreteSet<T>.CreateFromSortedArray(r.ToArray());
		}
		public ISet<T> GetIntersection(Interval<T> other)
		{
			int value = 0;
			int value2 = Values.Length - 1;
			if (!Infs.Findgeq(other.Sup, ref value2))
				return new EmptySet<T>();
			if (!Sups.Findleq(other.Inf, ref value))
				return new EmptySet<T>();
			if (value == value2)
				return Values[value].GetIntersection(other);
			List<Interval<T>> list = new List<Interval<T>>();
			List<T> list2 = new List<T>();
			ISet<T> intersection = Values[value].GetIntersection(other);
			if (intersection is Interval<T>)
				list.Add(intersection as Interval<T>);
			else if (intersection is DiscreteSet<T>)
				list2.AddRange((intersection as DiscreteSet<T>).Values);
			else if (!(intersection is EmptySet<T>))
				throw new Exception();
			for (int i = value + 1; i < value2; i++)
				list.Add(Values[i]);
			intersection = Values[value2].GetIntersection(other);
			if (intersection is Interval<T>)
				list.Add(intersection as Interval<T>);
			else if (intersection is DiscreteSet<T>)
				list2.AddRange((intersection as DiscreteSet<T>).Values);
			else if (!(intersection is EmptySet<T>))
				throw new Exception();
			if (list.Length == 0)
				return DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
			if (list2.Length == 0)
			{
				if (list.Length == 1)
					return list[0];
				return new Intervals<T>(list.ToArray());
			}
			DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
			if (list.Length == 1)
				return new Set<T>(discrete, list[0]);
			return new Set<T>(discrete, new Intervals<T>(list.ToArray()));
		}
		public ISet<T> GetIntersection(Intervals<T> other)
		{
			List<Interval<T>> list = new List<Interval<T>>();
			List<T> list2 = new List<T>();
			Interval<T>[] values = Values;
			foreach (Interval<T> other2 in values)
				foreach (ISet<T> item in other.GetIntersection(other2))
					switch(item)
                    {
						case Interval<T>: list.Add(item as Interval<T>);break;
						case DiscreteSet<T>: list2.AddRange((item as DiscreteSet<T>).Values);break;
						default:throw new Exception();
					}
			if (list.Length == 0)
			{
				if (list2.Length == 0) return new EmptySet<T>();
				else return DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
			}
			if (list2.Length == 0)
			{
				if (list.Length == 1)return list[0];
				else return new Intervals<T>(list.ToArray());
			}
			DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
			if (list.Length == 1)
				return new Set<T>(discrete, list[0]);
			return new Set<T>(discrete, new Intervals<T>(list.ToArray()));
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			for (int i = 0; i < Values.Length; i++)
				yield return Values[i];
		}
		IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();
		public static ISet<T> CreateFromArray(params Interval<T>[] values)
		{
			AVL<T, Interval<T>> r = new AVL<T, Interval<T>>();
			foreach (Interval<T> interval in values)
			{
				T inf = interval.Inf;
				if (r.ContainsKey(inf))
					r[inf] = interval.GetUnion(r[inf], out var _) as Interval<T>;
				else
					r[inf] = interval;
			}
			if (r.Length == 0)
				return new EmptySet<T>();
			if (r.Length == 1)
				return r.GetFirstValue();
			Interval<T>[] rs = r.LDRSort();
			Interval<T> temp = rs[0];
			List<Interval<T>> list = new List<Interval<T>>();
			for (int j = 1; j < rs.Length; j++)
			{
				bool adjacent;
				Interval<T> t = temp.GetUnion(rs[j], out adjacent) as Interval<T>;
				if (adjacent)
				{
					temp = t;
					continue;
				}
				list.Add(temp);
				temp = t;
			}
			list.Add(temp);
			return new Intervals<T>(list.ToArray());
		}
		public override string ToString()
			=>string.Join("+", (IEnumerable<Interval<T>>)Values);
		public bool Contain(T value)
		{
			int inf = 0;
			int sup = Values.Length - 1;
			if (!Infs.Findleq(value, ref inf))
				return false;
			if (!Sups.Findgeq(value, ref sup))
				return false;
			while (inf <= sup)
				if (Values[inf++].Contain(value))
					return true;
			return false;
		}
	}
}
