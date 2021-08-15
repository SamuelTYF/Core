using Collection.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class List<TValue> : ISerializable, IEnumerable<TValue>, IEnumerable
    {
        public TValue[] Values;
        public int Capacity;
        public int Length { get; private set; }
        public TValue this[int index]
        {
            get
            {
                CheckIndex(index);
                return Values[index];
            }
            set
            {
                CheckIndex(index);
                Values[index] = value;
            }
        }
        public List(int capcity = 16)
        {
            Capacity = (capcity == 0) ? 16 : capcity;
            Length = 0;
            Values = new TValue[Capacity];
        }
        public List(TValue[] values)
        {
            Values = values;
            Length = values.Length;
            if (Values.Length == 0)
                Values = new TValue[16];
            Capacity = Values.Length;
        }
        public List(IEnumerable<TValue> values)
            : this(16)
        {
            foreach (TValue value in values)
                Add(value);
        }
        public TValue[] ToArray()
        {
            if (Length == Capacity)
                return Values;
            TValue[] array = new TValue[Length];
            Array.Copy(Values, 0, array, 0, Length);
            return array;
        }
        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Length)
                throw new ArgumentOutOfRangeException(nameof(index));
        }
        public void CheckCapcity(int capacity)
        {
            if (Capacity < capacity)
            {
                while (Capacity < capacity)
                    Capacity <<= 1;
                TValue[] array = new TValue[Capacity];
                Array.Copy(Values, 0, array, 0, Length);
                Values = array;
            }
        }
        public void Add(TValue value)
        {
            CheckCapcity(Length + 1);
            Values[Length++] = value;
        }
        public void AddRange(params TValue[] values)
        {
            CheckCapcity(Length + values.Length);
            Array.Copy(values, 0, Values, Length, values.Length);
            Length += values.Length;
        }
        public void AddRange(IEnumerable<TValue> values)
        {
            foreach (TValue value in values)
                Add(value);
        }
        public void AddList(List<TValue> values, int index, int length)
        {
            CheckCapcity(Length + length);
            Array.Copy(values.Values, index, Values, Length, length);
            Length += values.Length;
        }
        public void AddList(List<TValue> values) => AddList(values, 0, values.Length);
        public TValue Pop() => Values[--Length];
        public TValue[] Pop(int length)
        {
            if (length > Length)
                throw new Exception();
            TValue[] array = new TValue[length];
            Length -= length;
            Array.Copy(Values, Length, array, 0, length);
            return array;
        }
        public void Remove(int index, int length = 1)
        {
            if (index + length > Length)
                throw new Exception();
            int num = index + length;
            Array.Copy(Values, num, Values, index, Length - num);
            Length -= length;
        }
        public List(Formatter formatter)
            : this((TValue[])formatter.Read())
        {
        }
        public void Write(Formatter formatter) => formatter.Write(ToArray());
        public override string ToString()
        {
            string s = typeof(TValue).Name + "[";
            if (Length != 0)
            {
                TValue val = Values[0];
                s += val;
                for (int i = 1; i < Length; i++)
                    s += "," + Values[i];
            }
            return s + "]";
        }
        public void CopyTo(int sourceindex, TValue[] destinationArray, int destinationindex, int length)
            => Array.Copy(Values, sourceindex, destinationArray, destinationindex, length);
        public void Clear() => Length = 0;
        public void Swap(int a, int b)
        {
            TValue val = Values[a];
            Values[a] = Values[b];
            Values[b] = val;
        }
        public IEnumerator<TValue> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return Values[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
