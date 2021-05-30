namespace Collection
{
	public class QueueNode<TValue>
	{
		public TValue Value;

		public QueueNode<TValue> Next;

		public QueueNode(TValue value, QueueNode<TValue> next = null)
		{
			Value = value;
			Next = next;
		}
	}
	public class QueueNode<T1, T2>
	{
		public T1 Value1;

		public T2 Value2;

		public QueueNode<T1, T2> Next;

		public QueueNode(T1 value1, T2 value2, QueueNode<T1, T2> next = null)
		{
			Value1 = value1;
			Value2 = value2;
			Next = next;
		}
	}
}
