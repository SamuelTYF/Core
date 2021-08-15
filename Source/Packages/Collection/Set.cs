using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class Set<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
    {
        public ISet<T> Intervals;
        public DiscreteSet<T> Discrete;
        public Set(DiscreteSet<T> discrete, Interval<T> interval)
        {
            Discrete = discrete;
            Intervals = interval;
        }
        public Set(DiscreteSet<T> discrete, Intervals<T> intervals)
        {
            Discrete = discrete;
            Intervals = intervals;
        }
        public Set(DiscreteSet<T> discrete, ISet<T> intervals)
        {
            if (intervals is not Interval<T> and not Intervals<T>)
            {
                throw new Exception();
            }
            Discrete = discrete;
            Intervals = intervals;
        }
        public IEnumerator<ISet<T>> GetEnumerator()
        {
            foreach (ISet<T> interval in Intervals)
            {
                yield return interval;
            }
            yield return Discrete;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool Contain(T value)
            => Intervals.Contain(value) || Discrete.Contain(value);
        public ISet<T> GetIntersection(Interval<T> other)
        {
            ISet<T> intersection = Intervals.GetIntersection(other);
            ISet<T> intersection2 = Discrete.GetIntersection(other);
            return intersection is EmptySet<T>
                ? intersection2
                : intersection2 is EmptySet<T> ? intersection : new Set<T>(intersection2 as DiscreteSet<T>, intersection);
        }
        public ISet<T> GetIntersection(DiscreteSet<T> other)
        {
            if (other.GetIntersection(Intervals) is not DiscreteSet<T> discreteSet)
                return other.GetIntersection(Discrete);
            if (discreteSet.Values.Length == other.Values.Length)
                return discreteSet;
            if (other.GetIntersection(Discrete) is not DiscreteSet<T> discreteSet2)
                return discreteSet;
            List<T> list = new();
            list.AddRange(discreteSet.Values);
            list.AddRange(discreteSet2.Values);
            return DiscreteSet<T>.CreateFromArray(list.ToArray());
        }
        public ISet<T> GetIntersection(Intervals<T> other)
        {
            ISet<T> intersection = Intervals.GetIntersection(other);
            ISet<T> intersection2 = Discrete.GetIntersection(other);
            return intersection is EmptySet<T>
                ? intersection2
                : intersection2 is EmptySet<T> ? intersection : new Set<T>(intersection2 as DiscreteSet<T>, intersection);
        }
        public ISet<T> GetIntersection(Set<T> other)
        {
            ISet<T> intersection = Intervals.GetIntersection(other.Intervals);
            List<T> list = new();
            if (other.Intervals.GetIntersection(Discrete) is DiscreteSet<T> discreteSet)
                list.AddRange(discreteSet.Values);
            if (Intervals.GetIntersection(other.Discrete) is DiscreteSet<T> discreteSet2)
                list.AddRange(discreteSet2.Values);
            if (Discrete.GetIntersection(other.Discrete) is DiscreteSet<T> discreteSet3)
                list.AddRange(discreteSet3.Values);
            if (list.Length == 0)
                return intersection;
            DiscreteSet<T> discreteSet4 = DiscreteSet<T>.CreateFromArray(list.ToArray()) as DiscreteSet<T>;
            return intersection is EmptySet<T> ? discreteSet4 : new Set<T>(discreteSet4, intersection);
        }
        public ISet<T> GetIntersection(ISet<T> other) => other switch
        {
            Interval<T> => GetIntersection(other as Interval<T>),
            DiscreteSet<T> => GetIntersection(other as DiscreteSet<T>),
            Intervals<T> => GetIntersection(other as Intervals<T>),
            Set<T> => GetIntersection(other as Set<T>),
            EmptySet<T> => new EmptySet<T>(),
            _ => throw new Exception()
        };
        public static ISet<T> CreateFromSets(params ISet<T>[] sets)
        {
            List<T> list = new();
            List<Interval<T>> list2 = new();
            foreach (ISet<T> set in sets)
            {
                foreach (ISet<T> item in set)
                {
                    if (item is Interval<T>)
                    {
                        list2.Add(item as Interval<T>);
                        continue;
                    }
                    if (item is DiscreteSet<T>)
                    {
                        list.AddRange((item as DiscreteSet<T>).Values);
                        continue;
                    }
                    throw new Exception();
                }
            }
            if (list.Length == 0)
                return Intervals<T>.CreateFromArray(list2.ToArray());
            if (list2.Length == 0)
                return DiscreteSet<T>.CreateFromArray(list.ToArray());
            DiscreteSet<T> discreteSet = DiscreteSet<T>.CreateFromArray(list.ToArray()) as DiscreteSet<T>;
            ISet<T> set2 = Intervals<T>.CreateFromArray(list2.ToArray());
            return discreteSet.DifferenceSet(set2) is not DiscreteSet<T> discreteSet2 ? set2 : new Set<T>(discreteSet2, set2);
        }
        public override string ToString()
            => $"{Intervals}+{Discrete}";
    }
}
