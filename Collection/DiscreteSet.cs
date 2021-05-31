using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class DiscreteSet<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
    {
        public T Inf;
        public T Sup;
        public T[] Values;
        public AVL<T, int> Indexes;
        private DiscreteSet(params T[] values)
        {
            Values = values;
            Inf = Values[0];
            Sup = Values[values.Length - 1];
            Indexes = new AVL<T, int>();
            for (int i = 0; i < values.Length; i++)
                Indexes[values[i]] = i;
        }
        public bool Contain(T value) => Indexes.ContainsKey(value);
        public bool IsIn(DiscreteSet<T> other)
        {
            if (Values.Length > other.Values.Length) return false;
            if (Inf.CompareTo(other.Inf) < 0) return false;
            if (Sup.CompareTo(other.Sup) > 0) return false;
            T[] values = Values;
            foreach (T value in values)
                if (!other.Contain(value))
                    return false;
            return true;
        }
        public ISet<T> GetIntersection(DiscreteSet<T> other)
        {
            int inf1 = 0;
            int sup1 = Values.Length - 1;
            if (!Indexes.Findgeq(other.Inf, ref inf1))
                return new EmptySet<T>();
            if (!Indexes.Findleq(other.Sup, ref sup1))
                return new EmptySet<T>();
            int inf2 = 0;
            int sup2 = other.Values.Length - 1;
            if (!other.Indexes.Findgeq(Inf, ref inf2))
                return new EmptySet<T>();
            if (!other.Indexes.Findleq(Sup, ref sup2))
                return new EmptySet<T>();
            List<T> r = new();
            while (inf1 <= sup1 && inf2 <= sup2)
            {
                int num = Values[inf1].CompareTo(other.Values[inf2]);
                if (num > 0)
                {
                    inf2++;
                    continue;
                }
                if (num < 0)
                {
                    inf1++;
                    continue;
                }
                r.Add(Values[inf1]);
                inf1++;
                inf2++;
            }
            return r.Length == 0 ? new EmptySet<T>() : new DiscreteSet<T>(r.ToArray());
        }
        public ISet<T> GetIntersection(Interval<T> other)
        {
            int inf1 = 0;
            int sup1 = Values.Length - 1;
            if (other.DownBlock)
                if (!Indexes.Findgeq(other.Inf, ref inf1))
                    return new EmptySet<T>();
                else if (!Indexes.Successor(other.Inf, ref inf1))
                    return new EmptySet<T>();
            if (other.UpBlock)
                if (!Indexes.Findleq(other.Sup, ref sup1))
                    return new EmptySet<T>();
                else if (!Indexes.Predecessor(other.Sup, ref sup1))
                    return new EmptySet<T>();
            if (inf1 > sup1)
                return new EmptySet<T>();
            if (inf1 == 0 && sup1 == Values.Length - 1)
                return this;
            T[] r = new T[sup1 - inf1 + 1];
            Array.Copy(Values, inf1, r, 0, r.Length);
            return new DiscreteSet<T>(r);
        }
        public ISet<T> GetIntersection(ISet<T> other) => other switch
        {
            DiscreteSet<T> => GetIntersection(other as DiscreteSet<T>),
            Interval<T> => GetIntersection(other as Interval<T>),
            _ => throw new NotImplementedException()
        };
        public static ISet<T> CreateFromSortedArray(params T[] values) => values.Length == 0 ? new EmptySet<T>() : new DiscreteSet<T>(values);
        public static ISet<T> CreateFromArray(params T[] values)
        {
            if (values.Length == 0)
                return new EmptySet<T>();
            AVL<T, bool> r = new();
            foreach (T key in values)
                r[key] = true;
            return new DiscreteSet<T>(r.LDROrder());
        }
        public override string ToString()
            => "{" + string.Join(",", Values) + "}";
        public IEnumerator<ISet<T>> GetEnumerator()
        {
            yield return this;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public DiscreteSet<T> Union(DiscreteSet<T> other)
        {
            AVL<T, bool> r = new();
            foreach (T key in Values)
                r[key] = true;
            foreach (T key in other.Values)
                r[key] = true;
            return new DiscreteSet<T>(r.LDROrder());
        }
        public ISet<T> DifferenceSet(Interval<T> other)
        {
            int value = 0;
            int value2 = Values.Length - 1;
            if (other.DownBlock)
                if (!Indexes.Findgeq(other.Inf, ref value))
                    return this;
                else if (!Indexes.Successor(other.Inf, ref value))
                    return this;
            if (other.UpBlock)
                if (!Indexes.Findleq(other.Sup, ref value2))
                    return this;
                else if (!Indexes.Predecessor(other.Sup, ref value2))
                    return this;
            if (value > value2)
                return this;
            if (value == 0 && value2 == Values.Length - 1)
                return new EmptySet<T>();
            T[] array = new T[Values.Length - (value2 - value + 1)];
            Array.Copy(Values, 0, array, 0, value);
            Array.Copy(Values, value2 + 1, array, value, Values.Length - value2 - 1);
            return new DiscreteSet<T>(array);
        }
        public ISet<T> DifferenceSet(Intervals<T> other)
        {
            DiscreteSet<T> r = this;
            foreach (Interval<T> value in other.Values)
                if ((r = r.DifferenceSet(value) as DiscreteSet<T>) == null)
                    return new EmptySet<T>();
            return r;
        }
        public ISet<T> DifferenceSet(ISet<T> other) => other switch
        {
            Interval<T> => DifferenceSet(other as Interval<T>),
            Intervals<T> => DifferenceSet(other as Intervals<T>),
            _ => throw new Exception()
        };
    }
}
