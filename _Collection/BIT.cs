namespace Collection
{
	public class BIT<T>
	{
		public T[] Values;

		public int Length;

		public Add<T> _Add;

		public Sub<T> _Sub;

		public BIT(Add<T> add, Sub<T> sub, int capacity = 1024)
		{
			_Add = add;
			_Sub = sub;
			Values = new T[Length = capacity];
		}

		public static int LowBit(int x)
		{
			return x & -x;
		}

		public void Add(int index, T value)
		{
			while (index < Length)
			{
				Values[index] = _Add(Values[index], value);
				index += LowBit(index);
			}
		}

		public void Sub(int index, T value)
		{
			while (index < Length)
			{
				Values[index] = _Sub(Values[index], value);
				index += LowBit(index);
			}
		}

		public T GetSum(int index)
		{
			return Values[index];
		}

		public T GetSum(int from, int to)
		{
			return _Sub(Values[to], Values[from - 1]);
		}
	}
}
