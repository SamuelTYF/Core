using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Collection.Serialization;

namespace Collection
{
	public class TrieTree<TValue> : ISerializable, IEnumerable<TValue>, IEnumerable
	{
		public TrieTree<TValue>[] Nodes;

		public TValue Value;

		public TValue this[string key, int index = 0]
		{
			get
			{
				return GetNode(Encoding.UTF8.GetBytes(key), index).Value;
			}
			set
			{
				GetNode(Encoding.UTF8.GetBytes(key), index).Value = value;
			}
		}

		public TValue this[byte[] key, int index = 0]
		{
			get
			{
				return GetNode(key, index).Value;
			}
			set
			{
				GetNode(key, index).Value = value;
			}
		}

		public TValue this[int key]
		{
			get
			{
				return GetNode(BitConverter.GetBytes(key)).Value;
			}
			set
			{
				GetNode(BitConverter.GetBytes(key)).Value = value;
			}
		}

		public TrieTree()
		{
			Nodes = new TrieTree<TValue>[256];
		}

		public TrieTree<TValue> GetNode(byte[] keys, int index = 0)
		{
			if (index == keys.Length)
			{
				return this;
			}
			byte b = keys[index];
			return (Nodes[b] ?? (Nodes[b] = new TrieTree<TValue>())).GetNode(keys, index + 1);
		}

		public TrieTree<TValue> GetNode(byte[] keys, int index, int length)
		{
			if (index == length)
			{
				return this;
			}
			byte b = keys[index];
			return (Nodes[b] ?? (Nodes[b] = new TrieTree<TValue>())).GetNode(keys, index + 1, length);
		}

		public TrieTree<TValue> GetNode(string key, int index = 0)
		{
			return GetNode(Encoding.UTF8.GetBytes(key), index);
		}

		private int Update()
		{
			int num = ((Value != null) ? 1 : 0);
			for (int i = 0; i < 256; i++)
			{
				if (Nodes[i] != null)
				{
					int num2 = Nodes[i].Update();
					if (num2 == 0)
					{
						Nodes[i] = null;
					}
					num += num2;
				}
			}
			return num;
		}

		public void Replace(CheckFunc<TrieTree<TValue>> check, TValue value)
		{
			if (check(this))
			{
				Value = value;
			}
			for (int i = 0; i < 256; i++)
			{
				if (Nodes[i] != null)
				{
					Nodes[i].Replace(check, value);
				}
			}
		}

		private void Write(List<byte> info, string temp, TValue[] values, ref int index)
		{
			if (Value != null)
			{
				info.Add(43);
				info.Add((byte)temp.Length);
				string text = temp;
				foreach (char c in text)
				{
					info.Add((byte)c);
				}
				values[index++] = Value;
				temp = "";
			}
			for (int j = 0; j < 256; j++)
			{
				if (Nodes[j] != null)
				{
					Nodes[j].Write(info, temp + (char)j, values, ref index);
				}
			}
			info.Add(45);
		}

		public TrieTree(Formatter formatter)
		{
			Nodes = new TrieTree<TValue>[256];
			byte[] array = formatter.Read() as byte[];
			TValue[] array2 = formatter.Read() as TValue[];
			int num = 0;
			Stack<TrieTree<TValue>> stack = new Stack<TrieTree<TValue>>();
			TrieTree<TValue> trieTree = this;
			int num2 = -1;
			if (array[array.Length - 1] != 45)
			{
				throw new Exception();
			}
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (num2 > 0)
				{
					stack.Insert(trieTree);
					trieTree = (trieTree.Nodes[array[i]] = new TrieTree<TValue>());
					num2--;
				}
				else if (array[i] == 43)
				{
					num2 = array[++i];
				}
				else
				{
					if (array[i] != 45)
					{
						throw new Exception();
					}
					trieTree = stack.Pop();
				}
				if (num2 == 0)
				{
					trieTree.Value = array2[num++];
					num2--;
				}
			}
			if (stack.Count != 0)
			{
				throw new Exception();
			}
		}

		public void Write(Formatter formatter)
		{
			int num = Update();
			TValue[] array = new TValue[num];
			int index = 0;
			List<byte> list = new List<byte>();
			Write(list, "", array, ref index);
			if (index != num)
			{
				throw new Exception();
			}
			formatter.Write(list.ToArray());
			formatter.Write(array);
		}

		public void Foreach(Foreach<TrieTree<TValue>> function)
		{
			if (Value != null && !Value.Equals(null))
			{
				function(this);
			}
			TrieTree<TValue>[] nodes = Nodes;
			for (int i = 0; i < nodes.Length; i++)
			{
				nodes[i]?.Foreach(function);
			}
		}

		public void Foreach(Foreach<string, TrieTree<TValue>> function, string suf = "")
		{
			if (Value != null && !Value.Equals(null))
			{
				function(suf, this);
			}
			for (int i = 0; i < 256; i++)
			{
				if (Nodes[i] != null)
				{
					Nodes[i].Foreach(function, suf + (char)i);
				}
			}
		}

		public static bool Equal(TrieTree<TValue> a, TrieTree<TValue> b, Equal<TValue> equal)
		{
			if (a == null)
			{
				if (b == null)
				{
					return true;
				}
				return Equal(b, a, equal);
			}
			if (b == null)
			{
				if (!equal(a.Value, default(TValue)))
				{
					return false;
				}
				for (int i = 0; i < 256; i++)
				{
					if (!Equal(a.Nodes[i], null, equal))
					{
						return false;
					}
				}
				return true;
			}
			if (!equal(a.Value, b.Value))
			{
				return false;
			}
			for (int j = 0; j < 256; j++)
			{
				if (!Equal(a.Nodes[j], b.Nodes[j], equal))
				{
					return false;
				}
			}
			return true;
		}

		private string GetString(string suf, string separator = ",")
		{
			string text = "";
			if (Value != null && !Value.Equals(null))
			{
				text += $"{suf}:{Value}";
			}
			bool flag = false;
			for (int i = 0; i < 256; i++)
			{
				if (Nodes[i] != null)
				{
					if (flag)
					{
						text += separator;
					}
					text += Nodes[i].GetString(suf + (char)i, separator);
					flag = true;
				}
			}
			return text;
		}

		public override string ToString()
		{
			return GetString("");
		}

		public string ToString(string separator = ",")
		{
			return GetString("", separator);
		}

		private TrieTree<TValue> _Copy()
		{
			TrieTree<TValue> trieTree = new TrieTree<TValue>();
			bool flag = true;
			if (Value != null && !Value.Equals(null))
			{
				flag = false;
				trieTree.Value = Value;
			}
			for (int i = 0; i < 256; i++)
			{
				if (Nodes[i] != null)
				{
					trieTree.Nodes[i] = Nodes[i]._Copy();
					if (trieTree.Nodes[i] != null)
					{
						flag = false;
					}
				}
			}
			return flag ? null : trieTree;
		}

		public TrieTree<TValue> Copy()
		{
			return _Copy() ?? new TrieTree<TValue>();
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			if (Value != null)
			{
				yield return Value;
			}
			TrieTree<TValue>[] nodes = Nodes;
			foreach (TrieTree<TValue> node in nodes)
			{
				if (node == null)
				{
					continue;
				}
				foreach (TValue item in node)
				{
					yield return item;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
