using System;

namespace Collection
{
	public static class BST_Algorithm
	{
		public static TKey[] DLROrder<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TKey[] keys = new TKey[tree.Length];
			int i = 0;
			tree.DLR((TKey key, TValue value)=>keys[i++] = key);
			return keys;
		}
		public static TValue[] DLRSort<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TValue[] values = new TValue[tree.Length];
			int i = 0;
			tree.DLR((TKey key, TValue value)=>values[i++] = value);
			return values;
		}
		public static TKey[] LDROrder<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TKey[] keys = new TKey[tree.Length];
			int i = 0;
			tree.LDR((TKey key, TValue value)=>keys[i++] = key);
			return keys;
		}
		public static TValue[] LDRSort<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TValue[] values = new TValue[tree.Length];
			int i = 0;
			tree.LDR((TKey key, TValue value)=>values[i++] = value);
			return values;
		}
		public static TKey[] LRDOrder<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TKey[] keys = new TKey[tree.Length];
			int i = 0;
			tree.LRD((TKey key, TValue value)=>keys[i++] = key);
			return keys;
		}
		public static TValue[] LRDSort<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TValue[] values = new TValue[tree.Length];
			int i = 0;
			tree.LRD((TKey key, TValue value)=>values[i++] = value);
			return values;
		}
		public static TKey[] RDLOrder<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TKey[] keys = new TKey[tree.Length];
			int i = 0;
			tree.RDL((TKey key, TValue value)=>keys[i++] = key);
			return keys;
		}
		public static TValue[] RDLSort<TKey, TValue>(this IBST<TKey, TValue> tree) where TKey : IComparable<TKey>
		{
			TValue[] values = new TValue[tree.Length];
			int i = 0;
			tree.RDL((TKey key, TValue value)=>values[i++] = value);
			return values;
		}
	}
}
