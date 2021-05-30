using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public class EmptySet<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		public bool IsIn(ISet<T> _) => true;
		public ISet<T> GetIntersection(ISet<T> other) => this;
		public override string ToString() => "ø";
		public IEnumerator<ISet<T>> GetEnumerator()
		{
			yield break;
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public bool Contain(T value) => false;
	}
}
