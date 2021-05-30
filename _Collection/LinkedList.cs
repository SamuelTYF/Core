using System;
using System.Collections;
using System.Collections.Generic;
using Collection.Serialization;

namespace Collection
{
	public class LinkedList<T> : IEnumerable<T>, IEnumerable, ISerializable
	{
		public LinkedListNode<T> Top;

		public LinkedListNode<T> Bottom;

		public int Length;

		public LinkedList()
		{
			Length = 0;
		}

		public LinkedList(params T[] values)
		{
			if ((Length = values.Length) != 0)
			{
				LinkedListNode<T> obj = new LinkedListNode<T>
				{
					Value = values[0]
				};
				LinkedListNode<T> linkedListNode = obj;
				Bottom = obj;
				LinkedListNode<T> linkedListNode2 = linkedListNode;
				for (int i = 1; i < values.Length; i++)
				{
					linkedListNode2 = (linkedListNode2.Next = new LinkedListNode<T>
					{
						Last = linkedListNode2,
						Value = values[i]
					});
				}
				Top = linkedListNode2;
			}
		}

		public LinkedList(IEnumerable<T> values)
		{
			LinkedListNode<T> linkedListNode = null;
			Length = 0;
			foreach (T value in values)
			{
				if (Length++ == 0)
				{
					LinkedListNode<T> obj = new LinkedListNode<T>
					{
						Value = value
					};
					LinkedListNode<T> linkedListNode2 = obj;
					Bottom = obj;
					linkedListNode = linkedListNode2;
				}
				else
				{
					LinkedListNode<T> linkedListNode3 = linkedListNode;
					LinkedListNode<T> obj2 = new LinkedListNode<T>
					{
						Last = linkedListNode,
						Value = value
					};
					LinkedListNode<T> linkedListNode2 = obj2;
					linkedListNode3.Next = obj2;
					linkedListNode = linkedListNode2;
				}
			}
			if (Length != 0)
			{
				Top = linkedListNode;
			}
		}

		public LinkedList(Formatter formatter)
			: this(formatter.Read() as T[])
		{
		}

		private LinkedListNode<T> Find(int index)
		{
			if (index < 0 || index > Length)
			{
				throw new IndexOutOfRangeException();
			}
			if (index << 1 < Length)
			{
				LinkedListNode<T> linkedListNode = Bottom;
				for (int i = 0; i < index; i++)
				{
					linkedListNode = linkedListNode.Next;
				}
				return linkedListNode;
			}
			LinkedListNode<T> linkedListNode2 = Top;
			for (int num = Length - 1; num > index; num--)
			{
				linkedListNode2 = linkedListNode2.Last;
			}
			return linkedListNode2;
		}

		public void Insert(int index, T value)
		{
			if (index == 0)
			{
				if (Length == 0)
				{
					LinkedListNode<T> obj = new LinkedListNode<T>
					{
						Value = value
					};
					LinkedListNode<T> top = obj;
					Bottom = obj;
					Top = top;
				}
				else
				{
					LinkedListNode<T> obj2 = new LinkedListNode<T>
					{
						Next = Bottom,
						Value = value
					};
					LinkedListNode<T> top = obj2;
					Bottom = obj2;
					top.Next.Last = Bottom;
				}
			}
			else if (index == Length)
			{
				LinkedListNode<T> obj3 = new LinkedListNode<T>
				{
					Last = Top,
					Value = value
				};
				LinkedListNode<T> top = obj3;
				Top = obj3;
				top.Last.Next = Top;
			}
			else
			{
				LinkedListNode<T> linkedListNode = Find(index);
				LinkedListNode<T> last = linkedListNode.Last;
				LinkedListNode<T> obj4 = new LinkedListNode<T>
				{
					Last = linkedListNode.Last,
					Next = linkedListNode,
					Value = value
				};
				LinkedListNode<T> top = obj4;
				last.Next = obj4;
				linkedListNode.Last = top;
			}
			Length++;
		}

		public int IndexOf(int index, T value, bool inverse = false)
		{
			LinkedListNode<T> linkedListNode = Find(index);
			if (inverse)
			{
				while (index >= 0)
				{
					if (linkedListNode.Value.Equals(value))
					{
						return index;
					}
					linkedListNode = linkedListNode.Last;
					index--;
				}
			}
			else
			{
				while (index < Length)
				{
					if (linkedListNode.Value.Equals(value))
					{
						return index;
					}
					linkedListNode = linkedListNode.Next;
					index++;
				}
			}
			return -1;
		}

		public T Get(int index)
		{
			return Find(index).Value;
		}

		public T Delete(int index)
		{
			if (Length == 0)
			{
				throw new Exception();
			}
			LinkedListNode<T> linkedListNode;
			if (index == 0)
			{
				linkedListNode = Bottom;
				if (Length == 1)
				{
					Top = (Bottom = null);
				}
				else
				{
					Bottom = Bottom.Next;
				}
			}
			else if (index == Length - 1)
			{
				Top = (linkedListNode = Top).Next;
			}
			else
			{
				((linkedListNode = Find(index)).Next.Last = linkedListNode.Last).Next = linkedListNode.Next;
			}
			Length--;
			return linkedListNode.Value;
		}

		public override string ToString()
		{
			return string.Join(",", ToArray());
		}

		public IEnumerator<T> GetEnumerator()
		{
			foreach (LinkedListNode<T> node in GetNodeEnumerator())
			{
				yield return node.Value;
			}
		}

		public T[] ToArray()
		{
			T[] array = new T[Length];
			LinkedListNode<T> linkedListNode = Bottom;
			for (int i = 0; i < Length; i++)
			{
				array[i] = linkedListNode.Value;
				linkedListNode = linkedListNode.Next;
			}
			return array;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<LinkedListNode<T>> GetNodeEnumerator()
		{
			LinkedListNode<T> T = Bottom;
			for (int i = 0; i < Length; i++)
			{
				yield return T;
				T = T.Next;
			}
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(ToArray());
		}
	}
}
