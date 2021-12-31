using Collection;
using System;
namespace Automata
{
    public class HashTable<T>
    {
        public List<(T, int)> Values;
        public int ModeCount => Values.Length;
        public T this[int hash]
        {
            get
            {
                for (int i = 0; i < ModeCount; i++)
                    if (Values[i].Item2 == hash)
                        return Values[i].Item1;
                throw new Exception();
            }
        }
        public HashTable() => Values = new();
        public void Clear() => Values.Clear();
        public int Register(T value, int hash)
        {
            int modeCount = ModeCount;
            for (int i = 0; i < modeCount; i++)
                if (Values[i].Item2 == hash)
                    return i;
            Values.Add((value, hash));
            return modeCount;
        }
        public T[] ToArray()
        {
            T[] r = new T[ModeCount];
            for (int i = 0; i < ModeCount; i++)
                r[i] = Values[i].Item1;
            return r;
        }
        public int[] GetHashArray()
        {
            int modeCount = ModeCount;
            int[] array = new int[modeCount];
            for (int i = 0; i < modeCount; i++)
                array[i] = Values[i].Item2;
            return array;
        }
    }
}
