using System;

namespace Collection
{
	public class CartesianTree<TKey, TValue> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
	{
		protected class Node
		{
			public Node L;

			public Node R;

			public TKey Key;

			public TValue Value;

			public Node(TKey key, TValue value)
			{
				Key = key;
				Value = value;
			}
		}

		protected Node Top;

		public CartesianTree(params (TKey, TValue)[] values)
		{
			Stack<Node> stack = new Stack<Node>();
			Node node;
			for (int i = 0; i < values.Length; i++)
			{
				(TKey, TValue) tuple = values[i];
				TKey item = tuple.Item1;
				TValue item2 = tuple.Item2;
				node = null;
				Node node2 = new Node(item, item2);
				while (stack.Count != 0 && stack.Get().Value.CompareTo(item2) == 1)
				{
					Node node3 = stack.Pop();
					node3.R = node;
					node = node3;
				}
				stack.Insert(node2);
				node2.L = node;
			}
			node = null;
			while (stack.Count != 0)
			{
				Node node3 = stack.Pop();
				node3.R = node;
				node = node3;
			}
			Top = node;
		}
	}
	public class CartesianTree<TValue> : CartesianTree<int, TValue> where TValue : IComparable<TValue>
	{
		public CartesianTree(params TValue[] values)
			: base(Array.Empty<(int, TValue)>())
		{
			Stack<Node> stack = new Stack<Node>();
			Node node;
			for (int i = 0; i < values.Length; i++)
			{
				node = null;
				Node node2 = new Node(i, values[i]);
				while (stack.Count != 0 && stack.Get().Value.CompareTo(values[i]) == 1)
				{
					Node node3 = stack.Pop();
					node3.R = node;
					node = node3;
				}
				stack.Insert(node2);
				node2.L = node;
			}
			node = null;
			while (stack.Count != 0)
			{
				Node node3 = stack.Pop();
				node3.R = node;
				node = node3;
			}
			Top = node;
		}
	}
}
