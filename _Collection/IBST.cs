using System;

namespace Collection
{
	public interface IBST<TKey, TValue> where TKey : IComparable<TKey>
	{
		TValue this[TKey key] { get; set; }

		int Length { get; }

		TValue Get(TKey key);

		void Set(TKey key, TValue value);

		void Remove(TKey key);

		bool Predecessor(TKey key, ref TValue value);

		bool Successor(TKey key, ref TValue value);

		void DLR(Foreach<TKey, TValue> func);

		void LDR(Foreach<TKey, TValue> func);

		void LRD(Foreach<TKey, TValue> func);

		void RDL(Foreach<TKey, TValue> func);
	}
	public interface IBST<T> where T : IComparable<T>
	{
		bool this[T key] { get; set; }

		int Length { get; }

		bool Get(T key);

		void Set(T key);

		void Remove(T key);

		bool Predecessor(T key, ref T value);

		bool Successor(T key, ref T value);

		void DLR(Foreach<T> func);

		void LDR(Foreach<T> func);

		void LRD(Foreach<T> func);

		void RDL(Foreach<T> func);
	}
}
