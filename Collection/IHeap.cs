using System;
using Collection.Serialization;

namespace Collection
{
	public interface IHeap<T> : ISerializable where T : IComparable<T>
	{
		int Length { get; }
		void Insert(T value);
		T Pop();
	}
}
