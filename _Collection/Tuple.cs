using Collection.Serialization;

namespace Collection
{
	public class Tuple<T1> : ISerializable
	{
		public T1 Value1;

		public Tuple(T1 value1)
		{
			Value1 = value1;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
		}
	}
	public class Tuple<T1, T2> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public Tuple(T1 value1, T2 value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
		}

		public static implicit operator (T1, T2)(Tuple<T1, T2> tuple)
		{
			return (tuple.Value1, tuple.Value2);
		}
	}
	public class Tuple<T1, T2, T3> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public Tuple(T1 value1, T2 value2, T3 value3)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
		}
	}
	public class Tuple<T1, T2, T3, T4> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public T4 Value4;

		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
			Value4 = (T4)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
			formatter.Write(Value4);
		}
	}
	public class Tuple<T1, T2, T3, T4, T5> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public T4 Value4;

		public T5 Value5;

		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
			Value5 = value5;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
			Value4 = (T4)formatter.Read();
			Value5 = (T5)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
			formatter.Write(Value4);
			formatter.Write(Value5);
		}
	}
	public class Tuple<T1, T2, T3, T4, T5, T6> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public T4 Value4;

		public T5 Value5;

		public T6 Value6;

		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
			Value5 = value5;
			Value6 = value6;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
			Value4 = (T4)formatter.Read();
			Value5 = (T5)formatter.Read();
			Value6 = (T6)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
			formatter.Write(Value4);
			formatter.Write(Value5);
			formatter.Write(Value6);
		}
	}
	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public T4 Value4;

		public T5 Value5;

		public T6 Value6;

		public T7 Value7;

		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
			Value5 = value5;
			Value6 = value6;
			Value7 = value7;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
			Value4 = (T4)formatter.Read();
			Value5 = (T5)formatter.Read();
			Value6 = (T6)formatter.Read();
			Value7 = (T7)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
			formatter.Write(Value4);
			formatter.Write(Value5);
			formatter.Write(Value6);
			formatter.Write(Value7);
		}
	}
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8> : ISerializable
	{
		public T1 Value1;

		public T2 Value2;

		public T3 Value3;

		public T4 Value4;

		public T5 Value5;

		public T6 Value6;

		public T7 Value7;

		public T8 Value8;

		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
			Value4 = value4;
			Value5 = value5;
			Value6 = value6;
			Value7 = value7;
			Value8 = value8;
		}

		public Tuple(Formatter formatter)
		{
			Value1 = (T1)formatter.Read();
			Value2 = (T2)formatter.Read();
			Value3 = (T3)formatter.Read();
			Value4 = (T4)formatter.Read();
			Value5 = (T5)formatter.Read();
			Value6 = (T6)formatter.Read();
			Value7 = (T7)formatter.Read();
			Value8 = (T8)formatter.Read();
		}

		public void Write(Formatter formatter)
		{
			formatter.Write(Value1);
			formatter.Write(Value2);
			formatter.Write(Value3);
			formatter.Write(Value4);
			formatter.Write(Value5);
			formatter.Write(Value6);
			formatter.Write(Value7);
			formatter.Write(Value8);
		}
	}
}
