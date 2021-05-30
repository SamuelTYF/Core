using System;

namespace Collection
{
	public sealed class SBT<TKey, TValue> : IBST<TKey, TValue> where TKey : IComparable<TKey>
	{
		private sealed class Node
		{
			public Node Parent;

			public Node L;

			public Node R;

			public int Size;

			public TKey Key;

			public TValue Value;

			public static readonly Node Null = new Node(null);

			public Node(Node parent)
			{
				Parent = parent;
				Size = 0;
				L = (R = Null);
			}

			public bool Find(TKey key, out Node node)
			{
				if (Size == 0)
				{
					node = this;
					return false;
				}
				return _Find(key, out node);
			}

			public void Remove(TKey key)
			{
				if (Size == 0)
				{
					throw new Exception();
				}
				Size--;
				switch (Key.CompareTo(key))
				{
				case 1:
					L.Remove(key);
					break;
				case 0:
					if (Size != 0)
					{
						if (L.Size == 0)
						{
							_Swap(R._FirstNode());
							R.Remove(key);
						}
						else
						{
							_Swap(L._LastNode());
							L.Remove(key);
						}
					}
					break;
				case -1:
					R.Remove(key);
					break;
				}
			}

			public Node Predecessor(Node temp, TKey key)
			{
				if (Size == 0)
				{
					return temp;
				}
				if (Key.CompareTo(key) == -1)
				{
					return R.Predecessor(this, key);
				}
				return L.Predecessor(temp, key);
			}

			public Node Successor(Node temp, TKey key)
			{
				if (Size == 0)
				{
					return temp;
				}
				if (Key.CompareTo(key) == 1)
				{
					return L.Successor(this, key);
				}
				return R.Successor(temp, key);
			}

			public void DLR(Foreach<Node> func)
			{
				func(this);
				if (L.Size > 0)
				{
					L.DLR(func);
				}
				if (R.Size > 0)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<Node> func)
			{
				if (L.Size > 0)
				{
					L.LDR(func);
				}
				func(this);
				if (R.Size > 0)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<Node> func)
			{
				if (L.Size > 0)
				{
					L.LRD(func);
				}
				if (R.Size > 0)
				{
					R.LRD(func);
				}
				func(this);
			}

			public void DLR(Foreach<TKey, TValue> func)
			{
				func(Key, Value);
				if (L.Size > 0)
				{
					L.DLR(func);
				}
				if (R.Size > 0)
				{
					R.DLR(func);
				}
			}

			public void LDR(Foreach<TKey, TValue> func)
			{
				if (L.Size > 0)
				{
					L.LDR(func);
				}
				func(Key, Value);
				if (R.Size > 0)
				{
					R.LDR(func);
				}
			}

			public void LRD(Foreach<TKey, TValue> func)
			{
				if (L.Size > 0)
				{
					L.LRD(func);
				}
				if (R.Size > 0)
				{
					R.LRD(func);
				}
				func(Key, Value);
			}

			public void RDL(Foreach<TKey, TValue> func)
			{
				if (R.Size > 0)
				{
					R.LRD(func);
				}
				func(Key, Value);
				if (L.Size > 0)
				{
					L.LRD(func);
				}
			}

			private bool _Find(TKey key, out Node node)
			{
				switch (key.CompareTo(Key))
				{
				case 1:
					if (R.Size > 0)
					{
						return R._Find(key, out node);
					}
					node = R;
					return false;
				case 0:
					if (L.Size > 0)
					{
						return L._Find(key, out node);
					}
					node = L;
					return false;
				default:
					node = this;
					return true;
				}
			}

			private Node _FirstNode()
			{
				Node node = this;
				while (node.Size > 0)
				{
					node = node.L;
				}
				return node.Parent;
			}

			private Node _LastNode()
			{
				Node node = this;
				while (node.Size > 0)
				{
					node = node.R;
				}
				return node.Parent;
			}

			private void _Swap(Node node)
			{
				TKey key = Key;
				TValue value = Value;
				Key = node.Key;
				Value = node.Value;
				node.Key = key;
				node.Value = value;
			}
		}

		private const int NullSize = 0;

		private Node Top;

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

		public SBT()
		{
			Top = new Node(null);
		}

		public bool ContainsKey(TKey key)
		{
			Node node;
			return Top.Find(key, out node);
		}

		public TValue Get(TKey key)
		{
			if (!Top.Find(key, out var node))
			{
				throw new Exception();
			}
			return node.Value;
		}

		public void Set(TKey key, TValue value)
		{
			Set(ref Top, key, value);
		}

		public void Remove(TKey key)
		{
			Top.Remove(key);
		}

		public override string ToString()
		{
			string s = "";
			Top.LDR(delegate(TKey key, TValue value)
			{
				s += $"Key:{key}\tValue:{value}\n";
			});
			return s;
		}

		public bool Predecessor(TKey key, ref TValue value)
		{
			Node node = Top.Predecessor(null, key);
			if (node != null)
			{
				value = node.Value;
				return true;
			}
			return false;
		}

		public bool Successor(TKey key, ref TValue value)
		{
			Node node = Top.Successor(null, key);
			if (node != null)
			{
				value = node.Value;
				return true;
			}
			return false;
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

		private void Maintain(ref Node node, bool flag)
		{
			if (flag)
			{
				if (node.R.R.Size > node.L.Size)
				{
					LeftRotate(ref node);
				}
				else
				{
					if (node.R.L.Size <= node.L.Size)
					{
						return;
					}
					RightRotate(ref node.R);
					LeftRotate(ref node);
				}
			}
			else if (node.L.L.Size > node.R.Size)
			{
				RightRotate(ref node);
			}
			else
			{
				if (node.L.R.Size <= node.R.Size)
				{
					return;
				}
				LeftRotate(ref node.L);
				RightRotate(ref node);
			}
			Maintain(ref node.L, flag: true);
			Maintain(ref node.R, flag: false);
		}

		private void LeftRotate(ref Node node)
		{
			Node r = node.R;
			r.Parent = node.Parent;
			node.Parent = r;
			node.R = r.L;
			r.L = node;
			r.Size = node.Size;
			node.Size = node.L.Size + node.R.Size + 1;
			node = r;
		}

		private void RightRotate(ref Node node)
		{
			Node l = node.L;
			l.Parent = node.Parent;
			node.Parent = l;
			node.L = l.R;
			l.R = node;
			l.Size = node.Size;
			node.Size = node.L.Size + node.R.Size + 1;
			node = l;
		}

		private void Set(ref Node node, TKey key, TValue value)
		{
			if (node.Size == 0)
			{
				node.Key = key;
				node.Value = value;
				node.Size++;
				node.L = new Node(node);
				node.R = new Node(node);
				for (Node parent = node.Parent; parent != null; parent = parent.Parent)
				{
					parent.Size++;
				}
				return;
			}
			switch (node.Key.CompareTo(key))
			{
			case 1:
				Set(ref node.L, key, value);
				Maintain(ref node, flag: false);
				break;
			case -1:
				Set(ref node.R, key, value);
				Maintain(ref node, flag: true);
				break;
			case 0:
				node.Value = value;
				break;
			}
		}
	}
}
