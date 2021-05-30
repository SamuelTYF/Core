using System;

namespace Collection
{
	public class Queue<TValue>
	{
		public QueueNode<TValue> Top;
		public QueueNode<TValue> End;
		public int Count;
		public Queue()
		{
			Count = 0;
			Top = End = null;
		}
		public TValue Pop()
		{
			if (Count == 0)
				throw new Exception();
			TValue value = Top.Value;
			Top = Top.Next;
			if (--Count == 0)
				End = null;
			return value;
		}
		public void Insert(TValue value)
		{
			if (Count++ == 0)
				Top = End = new QueueNode<TValue>(value);
			else
				End = End.Next = new QueueNode<TValue>(value);
		}
	}
}
