using System;

namespace Collection
{
	public sealed class Treap<TKey, TPhi, TValue> : IBST<TKey, TValue> where TKey : IComparable<TKey> where TPhi : IComparable<TPhi>
	{
		private sealed class Node
		{
			public Node L;

			public Node R;

			public TKey Key;

			public TPhi Phi;

			public TValue Value;

			public Node(TKey key, TPhi phi, TValue value)
			{
				Key = key;
				Phi = phi;
				Value = value;
			}

			public void DLR(Foreach<Node> func)
			{
				func(this);
				if (L != null)
				{
					L.DLR(func);
				}
				if (R != null)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<Node> func)
			{
				if (L != null)
				{
					L.LDR(func);
				}
				func(this);
				if (R != null)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<Node> func)
			{
				if (L != null)
				{
					L.LRD(func);
				}
				if (R != null)
				{
					R.LRD(func);
				}
				func(this);
			}

			public void DLR(Foreach<TKey, TValue> func)
			{
				func(Key, Value);
				if (L != null)
				{
					L.DLR(func);
				}
				if (R != null)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<TKey, TValue> func)
			{
				if (L != null)
				{
					L.LDR(func);
				}
				func(Key, Value);
				if (R != null)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<TKey, TValue> func)
			{
				if (L != null)
				{
					L.LRD(func);
				}
				if (R != null)
				{
					R.LRD(func);
				}
				func(Key, Value);
			}

			public void RDL(Foreach<TKey, TValue> func)
			{
				if (R != null)
				{
					R.LRD(func);
				}
				func(Key, Value);
				if (L != null)
				{
					L.LRD(func);
				}
			}
		}

		private Node Top;

		private PhiCreator<TPhi> Creator;

		public int Length { get; private set; }

		public TValue this[TKey key]
		{
			get
			{
				return Get(key);
			}
			set
			{
				Set(key, value);
			}
		}

		public Treap(PhiCreator<TPhi> creator)
		{
			Creator = creator;
		}

		private void SplitL(Node node, TKey key, ref Node l, ref Node r)
		{
			if (node == null)
			{
				l = (r = null);
			}
			else if (node.Key.CompareTo(key) == 1)
			{
				r = node;
				SplitL(r.L, key, ref l, ref r.L);
			}
			else
			{
				l = node;
				SplitL(l.R, key, ref l.R, ref r);
			}
		}

		private void SplitR(Node node, TKey key, ref Node l, ref Node r)
		{
			if (node == null)
			{
				l = (r = null);
			}
			else if (node.Key.CompareTo(key) == -1)
			{
				l = node;
				SplitR(l.R, key, ref l.R, ref r);
			}
			else
			{
				r = node;
				SplitR(r.L, key, ref l, ref r.L);
			}
		}

		private Node Merge(Node l, Node r)
		{
			if (l == null)
			{
				return r;
			}
			if (r == null)
			{
				return l;
			}
			if (l.Phi.CompareTo(r.Phi) == 1)
			{
				l.R = Merge(l.R, r);
				return l;
			}
			r.L = Merge(l, r.L);
			return r;
		}

		private Node Find(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			return node.Key.CompareTo(key) switch
			{
				1 => Find(node.L, key), 
				-1 => Find(node.R, key), 
				_ => node, 
			};
		}

		public TValue Get(TKey key)
		{
			Node node = Find(Top, key);
			if (node == null)
			{
				throw new Exception();
			}
			return node.Value;
		}

		public void Set(TKey key, TValue value)
		{
			Node node = Find(Top, key);
			if (node == null)
			{
				node = new Node(key, Creator(), value);
				Node r = null;
				SplitR(Top, key, ref Top, ref r);
				Top = Merge(Top, Merge(node, r));
			}
			else
			{
				node.Value = value;
			}
		}

		public void Remove(TKey key)
		{
			Node l = null;
			Node r = null;
			SplitR(Top, key, ref Top, ref r);
			SplitL(r, key, ref l, ref r);
			Top = Merge(Top, r);
			if (l == null)
			{
				throw new Exception();
			}
		}

		private Node _Predecessor(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			switch (node.Key.CompareTo(key))
			{
			case 1:
				return _Predecessor(node.L, key);
			case -1:
			{
				Node node2 = _Predecessor(node.R, key);
				if (node2 == null)
				{
					return node;
				}
				return node2;
			}
			default:
				return _Predecessor(node.L, key);
			}
		}

		private Node _Successor(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			switch (node.Key.CompareTo(key))
			{
			case -1:
				return _Successor(node.R, key);
			case 1:
			{
				Node node2 = _Successor(node.L, key);
				if (node2 == null)
				{
					return node;
				}
				return node2;
			}
			default:
				return _Successor(node.R, key);
			}
		}

		public bool Predecessor(TKey key, ref TValue value)
		{
			Node node = _Predecessor(Top, key);
			if (node == null)
			{
				return false;
			}
			value = node.Value;
			return true;
		}

		public bool Successor(TKey key, ref TValue value)
		{
			Node node = _Successor(Top, key);
			if (node == null)
			{
				return false;
			}
			value = node.Value;
			return true;
		}

		public void DLR(Foreach<TKey, TValue> func)
		{
			Top.DLR(func);
		}

		public void LDR(Foreach<TKey, TValue> func)
		{
			Top.LDR(func);
		}

		public void LRD(Foreach<TKey, TValue> func)
		{
			Top.LRD(func);
		}

		public void RDL(Foreach<TKey, TValue> func)
		{
			Top.RDL(func);
		}
	}
	public sealed class Treap<TKey, TPhi> where TKey : IComparable<TKey> where TPhi : IComparable<TPhi>
	{
		private sealed class Node
		{
			public Node L;

			public Node R;

			public TKey Key;

			public TPhi Phi;

			public Node(TKey key, TPhi phi)
			{
				Key = key;
				Phi = phi;
			}

			public void DLR(Foreach<Node> func)
			{
				func(this);
				if (L != null)
				{
					L.DLR(func);
				}
				if (R != null)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<Node> func)
			{
				if (L != null)
				{
					L.LDR(func);
				}
				func(this);
				if (R != null)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<Node> func)
			{
				if (L != null)
				{
					L.LRD(func);
				}
				if (R != null)
				{
					R.LRD(func);
				}
				func(this);
			}

			public void DLR(Foreach<TKey> func)
			{
				func(Key);
				if (L != null)
				{
					L.DLR(func);
				}
				if (R != null)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<TKey> func)
			{
				if (L != null)
				{
					L.LDR(func);
				}
				func(Key);
				if (R != null)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<TKey> func)
			{
				if (L != null)
				{
					L.LRD(func);
				}
				if (R != null)
				{
					R.LRD(func);
				}
				func(Key);
			}

			public void RDL(Foreach<TKey> func)
			{
				if (R != null)
				{
					R.LRD(func);
				}
				func(Key);
				if (L != null)
				{
					L.LRD(func);
				}
			}
		}

		private Node Top;

		private PhiCreator<TPhi> Creator;

		public int Length { get; private set; }

		public Treap(PhiCreator<TPhi> creator)
		{
			Creator = creator;
		}

		private void SplitL(Node node, TKey key, ref Node l, ref Node r)
		{
			if (node == null)
			{
				l = (r = null);
			}
			else if (node.Key.CompareTo(key) == 1)
			{
				r = node;
				SplitL(r.L, key, ref l, ref r.L);
			}
			else
			{
				l = node;
				SplitL(l.R, key, ref l.R, ref r);
			}
		}

		private void SplitR(Node node, TKey key, ref Node l, ref Node r)
		{
			if (node == null)
			{
				l = (r = null);
			}
			else if (node.Key.CompareTo(key) == -1)
			{
				l = node;
				SplitR(l.R, key, ref l.R, ref r);
			}
			else
			{
				r = node;
				SplitR(r.L, key, ref l, ref r.L);
			}
		}

		private Node Merge(Node l, Node r)
		{
			if (l == null)
			{
				return r;
			}
			if (r == null)
			{
				return l;
			}
			if (l.Phi.CompareTo(r.Phi) == 1)
			{
				l.R = Merge(l.R, r);
				return l;
			}
			r.L = Merge(l, r.L);
			return r;
		}

		private Node Find(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			return node.Key.CompareTo(key) switch
			{
				1 => Find(node.L, key), 
				-1 => Find(node.R, key), 
				_ => node, 
			};
		}

		public void Set(TKey key)
		{
			Node node = Find(Top, key);
			if (node == null)
			{
				Length++;
				node = new Node(key, Creator());
				Node r = null;
				SplitR(Top, key, ref Top, ref r);
				Top = Merge(Top, Merge(node, r));
			}
		}

		public bool Contain(TKey key)
		{
			return Find(Top, key) != null;
		}

		public void Remove(TKey key)
		{
			Node l = null;
			Node r = null;
			SplitR(Top, key, ref Top, ref r);
			SplitL(r, key, ref l, ref r);
			Top = Merge(Top, r);
			if (l == null)
			{
				throw new Exception();
			}
			Length--;
		}

		private Node _Predecessor(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			switch (node.Key.CompareTo(key))
			{
			case 1:
				return _Predecessor(node.L, key);
			case -1:
			{
				Node node2 = _Predecessor(node.R, key);
				if (node2 == null)
				{
					return node;
				}
				return node2;
			}
			default:
				return _Predecessor(node.L, key);
			}
		}

		private Node _Successor(Node node, TKey key)
		{
			if (node == null)
			{
				return null;
			}
			switch (node.Key.CompareTo(key))
			{
			case -1:
				return _Successor(node.R, key);
			case 1:
			{
				Node node2 = _Successor(node.L, key);
				if (node2 == null)
				{
					return node;
				}
				return node2;
			}
			default:
				return _Successor(node.R, key);
			}
		}

		public bool Predecessor(TKey key, ref TKey value)
		{
			Node node = _Predecessor(Top, key);
			if (node == null)
			{
				return false;
			}
			value = node.Key;
			return true;
		}

		public bool Successor(TKey key, ref TKey value)
		{
			Node node = _Successor(Top, key);
			if (node == null)
			{
				return false;
			}
			value = node.Key;
			return true;
		}

		public void DLR(Foreach<TKey> func)
		{
			if (Top != null)
			{
				Top.DLR(func);
			}
		}

		public void LDR(Foreach<TKey> func)
		{
			if (Top != null)
			{
				Top.LDR(func);
			}
		}

		public void LRD(Foreach<TKey> func)
		{
			if (Top != null)
			{
				Top.LRD(func);
			}
		}

		public void RDL(Foreach<TKey> func)
		{
			if (Top != null)
			{
				Top.RDL(func);
			}
		}

		public TKey[] LDRSort()
		{
			TKey[] keys = new TKey[Length];
			int i = 0;
			LDR(delegate(TKey key)
			{
				keys[i++] = key;
			});
			return keys;
		}
	}
}
