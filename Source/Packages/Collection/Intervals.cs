using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class Intervals<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
    {
        public Interval<T>[] Values;
        public AVL<T, int> Infs;
        public AVL<T, int> Sups;
        private Intervals(params Interval<T>[] values)
        {
            Values = values;
            Infs = new AVL<T, int>();
            Sups = new AVL<T, int>();
            for (int i = 0; i < values.Length; i++)
            {
                Infs[values[i].Inf] = i;
                Sups[values[i].Sup] = i;
            }
        }
        public ISet<T> GetIntersection(DiscreteSet<T> other)
        {
            int inf = 0;
            int sup = Values.Length - 1;
            if (!Infs.Findgeq(other.Sup, ref sup))
                return new EmptySet<T>();
            if (!Sups.Findleq(other.Inf, ref inf))
                return new EmptySet<T>();
            List<T> r = new();
            for (int i = inf; i <= sup; i++)
                if (other.GetIntersection(Values[i]) is DiscreteSet<T> discreteSet)
                    r.AddRange(discreteSet.Values);
            return r.Length == 0 ? new EmptySet<T>() : DiscreteSet<T>.CreateFromSortedArray(r.ToArray());
        }
        public ISet<T> GetIntersection(Interval<T> other)
        {
            int value = 0;
            int value2 = Values.Length - 1;
            if (!Infs.Findgeq(other.Sup, ref value2))
                return new EmptySet<T>();
            if (!Sups.Findleq(other.Inf, ref value))
                return new EmptySet<T>();
            if (value == value2)
                return Values[value].GetIntersection(other);
            List<Interval<T>> list = new();
            List<T> list2 = new();
            ISet<T> intersection = Values[value].GetIntersection(other);
            switch (intersection)
            {
                case Interval<T>:
                    list.Add(intersection as Interval<T>);
                    break;
                case DiscreteSet<T>:
                    list2.AddRange((intersection as DiscreteSet<T>).Values);
                    break;
                default:
                    if (intersection is not EmptySet<T>)
                        throw new Exception();
                    break;
            }
            for (int i = value + 1; i < value2; i++)
                list.Add(Values[i]);
            intersection = Values[value2].GetIntersection(other);
            switch (intersection)
            {
                case Interval<T>:
                    list.Add(intersection as Interval<T>);
                    break;
                case DiscreteSet<T>:
                    list2.AddRange((intersection as DiscreteSet<T>).Values);
                    break;
                default:
                    if (intersection is not EmptySet<T>)
                        throw new Exception();
                    break;
            }
            if (list.Length == 0)
                return DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
            if (list2.Length == 0)
            {
                return list.Length == 1 ? list[0] : new Intervals<T>(list.ToArray());
            }
            DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
            return list.Length == 1 ? new Set<T>(discrete, list[0]) : new Set<T>(discrete, new Intervals<T>(list.ToArray()));
        }
        public ISet<T> GetIntersection(Intervals<T> other)
        {
            List<Interval<T>> list = new();
            List<T> list2 = new();
            Interval<T>[] values = Values;
            foreach (Interval<T> other2 in values)
                foreach (ISet<T> item in other.GetIntersection(other2))
                    switch (item)
                    {
                        case Interval<T>: list.Add(item as Interval<T>); break;
                        case DiscreteSet<T>: list2.AddRange((item as DiscreteSet<T>).Values); break;
                        default: throw new Exception();
                    }
            if (list.Length == 0)
                return list2.Length == 0 ? new EmptySet<T>() : DiscreteSet<T>.CreateFromSortedArray(list2.ToArray());
            if (list2.Length == 0)
                return list.Length == 1 ? list[0] : new Intervals<T>(list.ToArray());
            DiscreteSet<T> discrete = DiscreteSet<T>.CreateFromSortedArray(list2.ToArray()) as DiscreteSet<T>;
            return list.Length == 1 ? new Set<T>(discrete, list[0]) : new Set<T>(discrete, new Intervals<T>(list.ToArray()));
        }
        public ISet<T> GetIntersection(ISet<T> other) => throw new NotImplementedException();
        public IEnumerator<ISet<T>> GetEnumerator()
        {
            for (int i = 0; i < Values.Length; i++)
                yield return Values[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static ISet<T> CreateFromArray(params Interval<T>[] values)
        {
            AVL<T, Interval<T>> r = new();
            foreach (Interval<T> interval in values)
            {
                T inf = interval.Inf;
                r[inf] = r.ContainsKey(inf) ? interval.GetUnion(r[inf], out var _) as Interval<T> : interval;
            }
            if (r.Length == 0)
                return new EmptySet<T>();
            if (r.Length == 1)
                return r.GetFirstValue();
            Interval<T>[] rs = r.LDRSort();
            Interval<T> temp = rs[0];
            List<Interval<T>> list = new();
            for (int j = 1; j < rs.Length; j++)
            {
                Interval<T> t = temp.GetUnion(rs[j], out bool adjacent) as Interval<T>;
                if (adjacent)
                {
                    temp = t;
                    continue;
                }
                list.Add(temp);
                temp = t;
            }
            list.Add(temp);
            return new Intervals<T>(list.ToArray());
        }
        public override string ToString()
            => string.Join("+", (IEnumerable<Interval<T>>)Values);
        public bool Contain(T value)
        {
            int inf = 0;
            int sup = Values.Length - 1;
            if (!Infs.Findleq(value, ref inf))
                return false;
            if (!Sups.Findgeq(value, ref sup))
                return false;
            while (inf <= sup)
                if (Values[inf++].Contain(value))
                    return true;
            return false;
        }
    }
}
