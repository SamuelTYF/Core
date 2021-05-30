using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class EmptySet<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public bool IsIn(ISet<T> _)
		{
			return true;
		}

		public ISet<T> GetIntersection(ISet<T> other)
		{
			return this;
		}

		public override string ToString()
		{
			return "ø";
		}

		public IEnumerator<ISet<T>> GetEnumerator()
		{
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Contain(T value)
		{
			return false;
		}
	}
}
