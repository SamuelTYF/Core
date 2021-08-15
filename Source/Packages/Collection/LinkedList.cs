using Collection.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class LinkedList<T> : IEnumerable<T>, IEnumerable, ISerializable
    {
        public LinkedListNode<T> Top;
        public LinkedListNode<T> Bottom;
        public int Length;
        public LinkedList() => Length = 0;
        public LinkedList(params T[] values)
        {
            if ((Length = values.Length) != 0)
            {
                LinkedListNode<T> now = new()
                {
                    Value = values[0]
                };
                Bottom = now;
                for (int i = 1; i < values.Length; i++)
                    now = now.Next = new LinkedListNode<T>
                    {
                        Last = now,
                        Value = values[i]
                    };
                Top = now;
            }
        }
        public LinkedList(IEnumerable<T> values)
        {
            LinkedListNode<T> now = null;
            Length = 0;
            foreach (T value in values)
                if (Length++ == 0)
                    Bottom = now = new LinkedListNode<T>
                    {
                        Value = value
                    };
                else
                    now = now.Next = new LinkedListNode<T>
                    {
                        Last = now,
                        Value = value
                    };
            if (Length != 0)
                Top = now;
        }
        public LinkedList(Formatter formatter)
            : this(formatter.Read() as T[])
        {
        }
        private LinkedListNode<T> Find(int index)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();
            if (index << 1 < Length)
            {
                LinkedListNode<T> now = Bottom;
                for (int i = 0; i < index; i++)
                    now = now.Next;
                return now;
            }
            else
            {
                LinkedListNode<T> now = Top;
                for (int num = Length - 1; num > index; num--)
                    now = now.Last;
                return now;
            }
        }
        public void Insert(int index, T value)
        {
            if (index == 0)
            {
                if (Length == 0)
                    Top = Bottom = new LinkedListNode<T>
                    {
                        Value = value
                    };
                else
                    Bottom.Last = Bottom = new LinkedListNode<T>
                    {
                        Next = Bottom,
                        Value = value
                    };
            }
            else if (index == Length)
                Top.Next = Top = new LinkedListNode<T>
                {
                    Last = Top,
                    Value = value
                };
            else
            {
                LinkedListNode<T> next = Find(index);
                LinkedListNode<T> last = next.Last;
                next.Last = last.Next = new LinkedListNode<T>
                {
                    Last = last,
                    Next = next,
                    Value = value
                };
            }
            Length++;
        }
        public int IndexOf(int index, T value, bool inverse = false)
        {
            LinkedListNode<T> now = Find(index);
            if (inverse)
                while (index >= 0)
                {
                    if (now.Value.Equals(value))
                        return index;
                    now = now.Last;
                    index--;
                }
            else
                while (index < Length)
                {
                    if (now.Value.Equals(value))
                        return index;
                    now = now.Next;
                    index++;
                }
            return -1;
        }
        public T Get(int index) => Find(index).Value;
        public T Delete(int index)
        {
            if (Length == 0)
                throw new Exception();
            LinkedListNode<T> now;
            if (index == 0)
            {
                now = Bottom;
                if (Length == 1)
                    Top = Bottom = null;
                else
                    Bottom = Bottom.Next;
            }
            else if (index == Length - 1)
                Top = (now = Top).Next;
            else
            {
                now = Find(index);
                now.Next.Last = now.Last;
                now.Last.Next = now.Next;
            }
            Length--;
            return now.Value;
        }
        public override string ToString() => string.Join(",", ToArray());
        public IEnumerator<T> GetEnumerator()
        {
            foreach (LinkedListNode<T> node in GetNodeEnumerator())
                yield return node.Value;
        }
        public T[] ToArray()
        {
            T[] values = new T[Length];
            LinkedListNode<T> now = Bottom;
            for (int i = 0; i < Length; i++)
            {
                values[i] = now.Value;
                now = now.Next;
            }
            return values;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerable<LinkedListNode<T>> GetNodeEnumerator()
        {
            LinkedListNode<T> T = Bottom;
            for (int i = 0; i < Length; i++)
            {
                yield return T;
                T = T.Next;
            }
        }
        public void Write(Formatter formatter) => formatter.Write(ToArray());
    }
}
