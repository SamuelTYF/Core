using System;

namespace Collection
{
	public class DoubleStackArray<TValue>
	{
		public TValue[] Values;

		public int StackPoint1;

		public int StackPoint2;

		public DoubleStackArray(int capacity)
		{
			Values = new TValue[capacity];
			StackPoint2 = capacity;
		}

		public void Stack1Push(TValue value)
		{
			if (StackPoint1 == StackPoint2)
			{
				throw new Exception();
			}
			Values[StackPoint1++] = value;
		}

		public void Stack2Push(TValue value)
		{
			if (StackPoint1 == StackPoint2)
			{
				throw new Exception();
			}
			Values[--StackPoint2] = value;
		}

		public TValue Stack1Pop()
		{
			if (StackPoint1 == 0)
			{
				throw new Exception();
			}
			return Values[--StackPoint1];
		}

		public TValue Stack2Pop()
		{
			if (StackPoint2 == Values.Length)
			{
				throw new Exception();
			}
			return Values[StackPoint2++];
		}
	}
}
