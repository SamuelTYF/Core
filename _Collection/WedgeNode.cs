using System;

namespace Collection
{
	public class WedgeNode<T> where T : IComparable<T>
	{
		public WedgeNode<T> Last;

		public WedgeNode<T> Next;

		public T Value;

		public int Index;

		public WedgeNode(WedgeNode<T> next, T value, int index)
		{
			Next = next;
			Value = value;
			Index = index;
		}
	}
}
