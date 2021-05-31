using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class Wedge<T> : IEnumerable<T>, IEnumerable where T : IComparable<T>
    {
        public WedgeNode<T> Top;
        public WedgeNode<T> End;
        public int Count;
        public int Mode;
        public Wedge(int mode)
        {
            Count = 0;
            Mode = mode;
        }
        public void Insert(T value, int index)
        {
            while (Count != 0 && End.Value.CompareTo(value).CompareTo(0) != Mode)
            {
                Count--;
                End = End.Next;
            }
            if (Count == 0)
                Top = End = new WedgeNode<T>(null, value, index);
            else
                End = End.Last = new WedgeNode<T>(End, value, index);
            Count++;
        }
        public void Pop(int index)
        {
            while (Count != 0 && Top.Index < index)
            {
                Count--;
                Top = Top.Last;
            }
            if (Count == 0)
                End = null;
        }
        public void Reset()
        {
            Count = 0;
            Top = End = null;
        }
        public override string ToString() => string.Join(",", this);
        public IEnumerator<T> GetEnumerator()
        {
            for (WedgeNode<T> i = Top; i != null; i = i.Last)
                yield return i.Value;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static T[] Filter(T[] values, int window, int mode)
        {
            int len = values.Length;
            T[] array = new T[len];
            Wedge<T> wedge = new(mode);
            int i = 0;
            int j = 0;
            while (i < window)
                wedge.Insert(values[i], i++);
            while (i < len)
            {
                array[j] = wedge.Top.Value;
                wedge.Pop(++j);
                wedge.Insert(values[i], i++);
            }
            while (j < len)
            {
                array[j] = wedge.Top.Value;
                wedge.Pop(++j);
            }
            return array;
        }
        public static T[] MinimumFilter(T[] values, int w)
        {
            int w2 = w << 1;
            int len = values.Length;
            T[] array = new T[len];
            Wedge<T> wedge = new(-1);
            int i = 0;
            int j = 0;
            int index = 0;
            while (i < w)
                wedge.Insert(values[i], i++);
            while (i < w2)
            {
                wedge.Insert(values[i], i++);
                array[j++] = wedge.Top.Value;
            }
            while (i < len)
            {
                wedge.Insert(values[i], i++);
                array[j++] = wedge.Top.Value;
                wedge.Pop(index++);
            }
            while (j < len)
            {
                array[j++] = wedge.Top.Value;
                wedge.Pop(index++);
            }
            return array;
        }
    }
}
