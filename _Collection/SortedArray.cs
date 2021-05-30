using System;
using System.Collections;
using System.Collections.Generic;
using Collection.Serialization;

namespace Collection
{
	public class SortedArray<T> : IEnumerable<T>, IEnumerable, ISerializable where T : IComparable<T>
	{
		private class SortedNode
		{
			public T Value;

			public int Index;

			public SortedNode Greater;

			public SortedNode Lesser;

			public SortedNode Get(T value)
			{
				int num = value.CompareTo(Value);
				if (num > 0)
				{
					return Greater?.Get(value);
				}
				if (num == 0)
				{
					return this;
				}
				return Lesser?.Get(value);
			}

			public SortedNode(T value, int index)
			{
				Value = value;
				Index = index;
			}
		}

		private SortedNode[] Nodes;

		public int Length;

		private SortedNode FirstNode;

		private SortedNode LastNode;

		public T Min;

		public T Max;

		public bool Contain(T value)
		{
			return LastNode.Get(value) != null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			SortedNode[] nodes = Nodes;
			foreach (SortedNode node in nodes)
			{
				yield return node.Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public SortedArray(Formatter formatter)
		{
			T[] array = formatter.Read() as T[];
			int[] array2 = formatter.Read() as int[];
			int[] array3 = formatter.Read() as int[];
			Length = array.Length;
			Nodes = new SortedNode[Length];
			for (int i = 0; i < Length; i++)
			{
				Nodes[i] = new SortedNode(array[i], i);
			}
			for (int j = 0; j < Length; j++)
			{
				if (array2[j] != -1)
				{
					Nodes[j].Greater = Nodes[array2[j]];
				}
				if (array3[j] != -1)
				{
					Nodes[j].Lesser = Nodes[array3[j]];
				}
			}
			Min = (FirstNode = Nodes[0]).Value;
			Max = (LastNode = Nodes[Length - 1]).Value;
		}

		public SortedArray(params T[] values)
		{
			Length = values.Length;
			if (Length == 0)
			{
				throw new Exception();
			}
			if (Length == 1)
			{
				Nodes = new SortedNode[1]
				{
					new SortedNode(values[0], 0)
				};
			}
			else
			{
				Nodes = new SortedNode[Length];
				int[] array = new int[Length - 1];
				int[] array2 = new int[Length - 1];
				for (int i = 0; i < Length; i++)
				{
					Nodes[i] = new SortedNode(values[i], i);
				}
				array[0] = Length - 1;
				List<int> list = new List<int> { 0 };
				while (list.Length != 0)
				{
					int num = list.Pop();
					SortedNode sortedNode = Nodes[num];
					if (array2[num] != num)
					{
						int num2 = array2[num] + num >> 1;
						if (num2 != array2[num])
						{
							array2[num2] = array2[num];
							array[num2] = num;
							sortedNode.Lesser = Nodes[num2];
							list.Add(num2);
						}
					}
					if (array[num] != num + 1)
					{
						int num3 = array[num] + num >> 1;
						if (num3 != num)
						{
							array2[num3] = num;
							array[num3] = array[num];
							sortedNode.Greater = Nodes[num3];
							list.Add(num3);
						}
					}
				}
				FirstNode = Nodes[0];
				LastNode = Nodes[Length - 1];
				LastNode.Lesser = FirstNode;
			}
			Min = values[0];
			Max = values[Length - 1];
		}

		public void Write(Formatter formatter)
		{
			T[] array = new T[Length];
			int[] array2 = new int[Length];
			int[] array3 = new int[Length];
			for (int i = 0; i < Length; i++)
			{
				SortedNode sortedNode = Nodes[i];
				array[i] = sortedNode.Value;
				array2[i] = ((sortedNode.Greater == null) ? (-1) : sortedNode.Greater.Index);
				array3[i] = ((sortedNode.Lesser == null) ? (-1) : sortedNode.Lesser.Index);
			}
			formatter.Write(array);
			formatter.Write(array2);
			formatter.Write(array3);
		}

		public override string ToString()
		{
			return "[" + string.Join(",", this) + "]";
		}
	}
}
