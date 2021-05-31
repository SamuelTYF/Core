using System;
using System.Collections;
using System.Collections.Generic;
namespace Collection
{
    public class Interval<T> : ISet<T>, IEnumerable<ISet<T>>, IEnumerable where T : IComparable<T>
    {
        public T Inf;
        public T Sup;
        public bool DownBlock;
        public bool UpBlock;
        private Interval(T inf, T sup, bool downblock, bool upblock)
        {
            Inf = inf;
            Sup = sup;
            DownBlock = downblock;
            UpBlock = upblock;
        }
        public override string ToString()
            => $"{(DownBlock ? '[' : '(')}{Inf},{Sup}{(UpBlock ? ']' : ')')}";
        public bool Contain(T value)
        {
            int num = value.CompareTo(Inf);
            if (num < 0)return false;
            if (num == 0)
                return DownBlock;
            num = value.CompareTo(Sup);
            return num == 0 ? UpBlock : num < 0;
        }
        public bool IsIn(Interval<T> other)
        {
            int num = Inf.CompareTo(other.Inf);
            if (num < 0)
            {
                return false;
            }
            if (num == 0 && DownBlock && !other.DownBlock)
            {
                return false;
            }
            num = Sup.CompareTo(other.Sup);
            return num switch
            {
                > 0 => false,
                0 when UpBlock && !other.UpBlock => false,
                _ => true
            };
        }
        public bool IsEqual(Interval<T> other)
            => Inf.CompareTo(other.Inf) == 0 && DownBlock == other.DownBlock && Sup.CompareTo(other.Inf) == 0 && UpBlock == other.UpBlock;
        public bool IsIn(DiscreteSet<T> _)
            => false;
        public ISet<T> GetIntersection(Interval<T> other)
        {
            int num = Inf.CompareTo(other.Inf);
            int num2 = Sup.CompareTo(other.Sup);
            return num switch
            {
                > 0 => num2 switch
                {
                    > 0 => CreateFromRange(Inf, other.Sup, DownBlock, other.UpBlock),
                    0 => CreateFromRange(Inf, Sup, DownBlock, UpBlock && other.UpBlock),
                    _ => CreateFromRange(Inf, Sup, DownBlock, UpBlock)
                },
                0 => num2 switch
                {
                    > 0 => CreateFromRange(Inf, other.Sup, DownBlock && other.DownBlock, other.UpBlock),
                    0 => CreateFromRange(Inf, Sup, DownBlock && other.DownBlock, UpBlock && other.UpBlock),
                    _ => CreateFromRange(Inf, Sup, DownBlock && other.DownBlock, UpBlock)
                },
                _ => num2 switch
                {
                    > 0 => CreateFromRange(other.Inf, other.Sup, other.DownBlock, other.UpBlock),
                    0 => CreateFromRange(other.Inf, Sup, other.DownBlock, UpBlock && other.UpBlock),
                    _ => CreateFromRange(other.Inf, Sup, other.DownBlock, UpBlock)
                }
            };
        }
        public static ISet<T> CreateFromRange(T inf, T sup, bool downblock, bool upblock) => inf.CompareTo(sup) switch
        {
            > 0 => new EmptySet<T>(),
            0 => downblock && upblock ? DiscreteSet<T>.CreateFromSortedArray(inf) : new EmptySet<T>(),
            _ => new Interval<T>(inf, sup, downblock, upblock)
        };

        public ISet<T> GetIntersection(ISet<T> other) => other is Interval<T> ? GetIntersection(other as Interval<T>) : other.GetIntersection(this);
        public IEnumerator<ISet<T>> GetEnumerator()
        {
            yield return this;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public ISet<T> GetUnion(Interval<T> other, out bool adjacent)
        {
            int num = Inf.CompareTo(other.Inf);
            int num2 = Sup.CompareTo(other.Sup);
            T val;
            T inf;
            bool flag;
            bool downblock;
            switch (num)
            {
                case > 0:
                    val = Inf;
                    inf = other.Inf;
                    flag = DownBlock;
                    downblock = other.DownBlock;
                    break;
                case 0:
                    val = inf = Inf;
                    downblock = flag = DownBlock && other.DownBlock;
                    break;
                default:
                    val = other.Inf;
                    inf = Inf;
                    flag = other.DownBlock;
                    downblock = DownBlock;
                    break;
            }
            T sup;
            T sup2;
            bool upblock;
            bool flag2;
            switch (num2)
            {
                case > 0:
                    sup = Sup;
                    sup2 = other.Sup;
                    upblock = UpBlock;
                    flag2 = other.UpBlock;
                    break;
                case 0:
                    sup = sup2 = Sup;
                    flag2 = upblock = UpBlock && other.UpBlock;
                    break;
                default:
                    sup = other.Sup;
                    sup2 = Sup;
                    upblock = other.UpBlock;
                    flag2 = UpBlock;
                    break;
            }
            int num3 = val.CompareTo(sup2);
            adjacent = num3 > 0 || num3 == 0 && (flag || flag2);
            return adjacent ? CreateFromRange(inf, sup, downblock, upblock) : Intervals<T>.CreateFromArray(this, other);
        }
    }
}
