using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class Set<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public ISet<T> Intervals;

		public DiscreteSet<T> Discrete;

		public Set(DiscreteSet<T> discrete, Interval<T> interval)
		{
			Discrete = discrete;
			Intervals = interval;
		}

		public Set(DiscreteSet<T> discrete, Intervals<T> intervals)
		{
			Discrete = discrete;
			Intervals = intervals;
		}

		public Set(DiscreteSet<T> discrete, ISet<T> intervals)
		{
			if (!(intervals is Interval<T>) && !(intervals is Intervals<T>))
			{
				throw new Exception();
			}
			Discrete = discrete;
			Intervals = intervals;
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			foreach (ISet<T> interval in Intervals)
			{
				yield return interval;
			}
			yield return Discrete;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Contain(T value)
		{
			return Intervals.Contain(value) || Discrete.Contain(value);
		}

		public ISet<T> GetIntersection(Interval<T> other)
		{
			ISet<T> intersection = Intervals.GetIntersection(other);
			ISet<T> intersection2 = Discrete.GetIntersection(other);
			if (intersection is EmptySet<T>)
			{
				return intersection2;
			}
			if (intersection2 is EmptySet<T>)
			{
				return intersection;
			}
			return new Set<T>(intersection2 as DiscreteSet<T>, intersection);
		}

		public ISet<T> GetIntersection(DiscreteSet<T> other)
		{
			DiscreteSet<T> discreteSet = other.GetIntersection(Intervals) as DiscreteSet<T>;
			if (discreteSet == null)
			{
				return other.GetIntersection(Discrete);
			}
			if (discreteSet.Values.Length == other.Values.Length)
			{
				return discreteSet;
			}
			DiscreteSet<T> discreteSet2 = other.GetIntersection(Discrete) as DiscreteSet<T>;
			if (discreteSet2 == null)
			{
				return discreteSet;
			}
			List<T> list = new List<T>();
			list.AddRange(discreteSet.Values);
			list.AddRange(discreteSet2.Values);
			return DiscreteSet<T>.CreateFromArray(list.ToArray());
		}

		public ISet<T> GetIntersection(Intervals<T> other)
		{
			ISet<T> intersection = Intervals.GetIntersection(other);
			ISet<T> intersection2 = Discrete.GetIntersection(other);
			if (intersection is EmptySet<T>)
			{
				return intersection2;
			}
			if (intersection2 is EmptySet<T>)
			{
				return intersection;
			}
			return new Set<T>(intersection2 as DiscreteSet<T>, intersection);
		}

		public ISet<T> GetIntersection(Set<T> other)
		{
			ISet<T> intersection = Intervals.GetIntersection(other.Intervals);
			DiscreteSet<T> discreteSet = other.Intervals.GetIntersection(Discrete) as DiscreteSet<T>;
			DiscreteSet<T> discreteSet2 = Intervals.GetIntersection(other.Discrete) as DiscreteSet<T>;
			DiscreteSet<T> discreteSet3 = Discrete.GetIntersection(other.Discrete) as DiscreteSet<T>;
			List<T> list = new List<T>();
			if (discreteSet != null)
			{
				list.AddRange(discreteSet.Values);
			}
			if (discreteSet2 != null)
			{
				list.AddRange(discreteSet2.Values);
			}
			if (discreteSet3 != null)
			{
				list.AddRange(discreteSet3.Values);
			}
			if (list.Length == 0)
			{
				return intersection;
			}
			DiscreteSet<T> discreteSet4 = DiscreteSet<T>.CreateFromArray(list.ToArray()) as DiscreteSet<T>;
			if (intersection is EmptySet<T>)
			{
				return discreteSet4;
			}
			return new Set<T>(discreteSet4, intersection);
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			if (other is Interval<T>)
			{
				return GetIntersection(other as Interval<T>);
			}
			if (other is DiscreteSet<T>)
			{
				return GetIntersection(other as DiscreteSet<T>);
			}
			if (other is Intervals<T>)
			{
				return GetIntersection(other as Intervals<T>);
			}
			if (other is Set<T>)
			{
				return GetIntersection(other as Set<T>);
			}
			if (other is EmptySet<T>)
			{
				return new EmptySet<T>();
			}
			throw new Exception();
		}

		public static ISet<T> CreateFromSets(params ISet<T>[] sets)
		{
			List<T> list = new List<T>();
			List<Interval<T>> list2 = new List<Interval<T>>();
			foreach (ISet<T> set in sets)
			{
				foreach (ISet<T> item in set)
				{
					if (item is Interval<T>)
					{
						list2.Add(item as Interval<T>);
						continue;
					}
					if (item is DiscreteSet<T>)
					{
						list.AddRange((item as DiscreteSet<T>).Values);
						continue;
					}
					throw new Exception();
				}
			}
			if (list.Length == 0)
			{
				return Intervals<T>.CreateFromArray(list2.ToArray());
			}
			if (list2.Length == 0)
			{
				return DiscreteSet<T>.CreateFromArray(list.ToArray());
			}
			DiscreteSet<T> discreteSet = DiscreteSet<T>.CreateFromArray(list.ToArray()) as DiscreteSet<T>;
			ISet<T> set2 = Intervals<T>.CreateFromArray(list2.ToArray());
			DiscreteSet<T> discreteSet2 = discreteSet.DifferenceSet(set2) as DiscreteSet<T>;
			if (discreteSet2 == null)
			{
				return set2;
			}
			return new Set<T>(discreteSet2, set2);
		}

		public override string ToString()
		{
			return $"{Intervals}+{Discrete}";
		}
	}
}
