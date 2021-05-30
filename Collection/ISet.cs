using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
	public interface ISet<T> : IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
	{
		bool Contain(T value);
		ISet<T> GetIntersection(ISet<T> other);
	}
}
