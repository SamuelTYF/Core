using Collection.Serialization;
using System;
namespace Collection
{
    public class Heap<T> : IHeap<T>, ISerializable where T : IComparable<T>
    {
        public List<T> Values;
        public int Mode;
        public int Length { get; private set; }
        public Heap(int mode)
        {
            Values = new List<T>
            {
                default
            };
            Length = 0;
            Mode = mode;
        }
        public void Swap(int a, int b)
        {
            T value = Values[a];
            Values[a] = Values[b];
            Values[b] = value;
        }
        public void Up(int x)
        {
            if (x != 1)
            {
                int num = x >> 1;
                if (Values[x].CompareTo(Values[num]) == Mode)
                {
                    Swap(x, num);
                    Up(num);
                }
            }
        }
        public void Insert(T value)
        {
            Values.Add(value);
            Up(++Length);
        }
        public void Down(int x)
        {
            int num = x << 1;
            int num2 = num | 1;
            if (num2 > Length)
            {
                if (num == Length && Values[num].CompareTo(Values[x]) == Mode)
                {
                    Swap(num, x);
                }
                return;
            }
            int num3 = ((Values[num].CompareTo(Values[num2]) == Mode) ? num : num2);
            if (Values[num3].CompareTo(Values[x]) == Mode)
            {
                Swap(num3, x);
                Down(num3);
            }
        }
        public T Pop()
        {
            T result = Values[1];
            Swap(1, Length--);
            Down(1);
            return result;
        }
        public override string ToString()
        {
            T[] array = new T[Length];
            Values.CopyTo(1, array, 0, Length);
            return "[" + string.Join(",", array) + "]";
        }
        public void Write(Formatter formatter)
            => formatter.Write(Values.ToArray());
        public Heap(Formatter formatter) => Length = (Values = new(formatter.Read() as T[])).Length - 1;
    }
    public class Heap<TKey, TValue> where TKey : IComparable<TKey>
    {
        public List<TKey> Keys;
        public List<TValue> Values;
        public int Mode;
        public int Length { get; private set; }
        public Heap(int mode)
        {
            Keys = new List<TKey>
            {
                default
            };
            Values = new List<TValue>
            {
                default
            };
            Length = 0;
            Mode = mode;
        }
        public void Swap(int a, int b)
        {
            TKey value = Keys[a];
            Keys[a] = Keys[b];
            Keys[b] = value;
            TValue value2 = Values[a];
            Values[a] = Values[b];
            Values[b] = value2;
        }
        public void Up(int x)
        {
            if (x != 1)
            {
                int num = x >> 1;
                if (Keys[x].CompareTo(Keys[num]) == Mode)
                {
                    Swap(x, num);
                    Up(num);
                }
            }
        }
        public void Insert(TKey key, TValue value)
        {
            Keys.Add(key);
            Values.Add(value);
            Up(++Length);
        }
        public void Down(int x)
        {
            int num = x << 1;
            int num2 = num | 1;
            if (num2 > Length)
            {
                if (num == Length && Keys[num].CompareTo(Keys[x]) == Mode)
                {
                    Swap(num, x);
                }
                return;
            }
            int num3 = ((Keys[num].CompareTo(Keys[num2]) == Mode) ? num : num2);
            if (Keys[num3].CompareTo(Keys[x]) == Mode)
            {
                Swap(num3, x);
                Down(num3);
            }
        }
        public TValue Pop(out TKey key)
        {
            key = Keys[1];
            TValue result = Values[1];
            Swap(1, Length--);
            Down(1);
            return result;
        }
        public override string ToString()
        {
            string[] array = new string[Length];
            for (int i = 1; i <= Length; i++)
            {
                array[i] = $"({Keys[i]},{Values[i]})";
            }
            return "[" + string.Join(",", array) + "]";
        }
    }
}
