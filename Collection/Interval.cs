using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class Interval<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public T Inf;

		public T Sup;

		public bool DownBlock;

		public bool UpBlock;

		private Interval(T inf, T sup, bool downblock, bool upblock)
		{
			Inf = inf;
			Sup = sup;
			DownBlock = downblock;
			UpBlock = upblock;
		}

		public override string ToString()
		{
			return $"{(DownBlock ? '[' : '(')}{Inf},{Sup}{(UpBlock ? ']' : ')')}";
		}

		public bool Contain(T value)
		{
			int num = value.CompareTo(Inf);
			if (num < 0)
			{
				return false;
			}
			if (num == 0)
			{
				return DownBlock;
			}
			num = value.CompareTo(Sup);
			if (num == 0)
			{
				return UpBlock;
			}
			return num < 0;
		}

		public bool IsIn(Interval<T> other)
		{
			int num = Inf.CompareTo(other.Inf);
			if (num < 0)
			{
				return false;
			}
			if (num == 0 && DownBlock && !other.DownBlock)
			{
				return false;
			}
			num = Sup.CompareTo(other.Sup);
			if (num > 0)
			{
				return false;
			}
			if (num == 0 && UpBlock && !other.UpBlock)
			{
				return false;
			}
			return true;
		}

		public bool IsEqual(Interval<T> other)
		{
			return Inf.CompareTo(other.Inf) == 0 && DownBlock == other.DownBlock && Sup.CompareTo(other.Inf) == 0 && UpBlock == other.UpBlock;
		}

		public bool IsIn(DiscreteSet<T> _)
		{
			return false;
		}

		public ISet<T> GetIntersection(Interval<T> other)
		{
			int num = Inf.CompareTo(other.Inf);
			int num2 = Sup.CompareTo(other.Sup);
			if (num > 0)
			{
				if (num2 > 0)
				{
					return CreateFromRange(Inf, other.Sup, DownBlock, other.UpBlock);
				}
				if (num2 == 0)
				{
					return CreateFromRange(Inf, Sup, DownBlock, UpBlock && other.UpBlock);
				}
				return CreateFromRange(Inf, Sup, DownBlock, UpBlock);
			}
			if (num == 0)
			{
				if (num2 > 0)
				{
					return CreateFromRange(Inf, other.Sup, DownBlock && other.DownBlock, other.UpBlock);
				}
				if (num2 == 0)
				{
					return CreateFromRange(Inf, Sup, DownBlock && other.DownBlock, UpBlock && other.UpBlock);
				}
				return CreateFromRange(Inf, Sup, DownBlock && other.DownBlock, UpBlock);
			}
			if (num2 > 0)
			{
				return CreateFromRange(other.Inf, other.Sup, other.DownBlock, other.UpBlock);
			}
			if (num2 == 0)
			{
				return CreateFromRange(other.Inf, Sup, other.DownBlock, UpBlock && other.UpBlock);
			}
			return CreateFromRange(other.Inf, Sup, other.DownBlock, UpBlock);
		}

		public static ISet<T> CreateFromRange(T inf, T sup, bool downblock, bool upblock)
		{
			int num = inf.CompareTo(sup);
			if (num > 0)
			{
				return new EmptySet<T>();
			}
			if (num == 0)
			{
				if (downblock && upblock)
				{
					return DiscreteSet<T>.CreateFromSortedArray(inf);
				}
				return new EmptySet<T>();
			}
			return new Interval<T>(inf, sup, downblock, upblock);
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			if (other is Interval<T>)
			{
				return GetIntersection(other as Interval<T>);
			}
			return other.GetIntersection(this);
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			yield return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public ISet<T> GetUnion(Interval<T> other, out bool adjacent)
		{
			int num = Inf.CompareTo(other.Inf);
			int num2 = Sup.CompareTo(other.Sup);
			T val;
			T inf;
			bool flag;
			bool downblock;
			if (num > 0)
			{
				val = Inf;
				inf = other.Inf;
				flag = DownBlock;
				downblock = other.DownBlock;
			}
			else if (num == 0)
			{
				val = (inf = Inf);
				downblock = (flag = DownBlock && other.DownBlock);
			}
			else
			{
				val = other.Inf;
				inf = Inf;
				flag = other.DownBlock;
				downblock = DownBlock;
			}
			T sup;
			T sup2;
			bool upblock;
			bool flag2;
			if (num2 > 0)
			{
				sup = Sup;
				sup2 = other.Sup;
				upblock = UpBlock;
				flag2 = other.UpBlock;
			}
			else if (num2 == 0)
			{
				sup = (sup2 = Sup);
				flag2 = (upblock = UpBlock && other.UpBlock);
			}
			else
			{
				sup = other.Sup;
				sup2 = Sup;
				upblock = other.UpBlock;
				flag2 = UpBlock;
			}
			int num3 = val.CompareTo(sup2);
			adjacent = num3 > 0 || (num3 == 0 && (flag || flag2));
			if (adjacent)
			{
				return CreateFromRange(inf, sup, downblock, upblock);
			}
			return Intervals<T>.CreateFromArray(this, other);
		}
	}
}
