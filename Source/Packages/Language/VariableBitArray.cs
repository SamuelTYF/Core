using System;
using System.Collections;
using System.Collections.Generic;
namespace Language
{
    public class VariableBitArray<T>:IComparable<VariableBitArray<T>>,IEnumerable<Variable<T>>
    {
        public IAssemble<Variable<T>> Variables;
        public bool[] BitArray;
        public int Count;
        public bool End;
        public VariableBitArray(IAssemble<Variable<T>> variables)
        {
            Variables = variables;
            BitArray = new bool[variables.Count];
            Count = 0;
            End = false;
        }
        public void Set(Variable<T> variable)
        {
            if(!BitArray[variable.Index])
            {
                Count++;
                BitArray[variable.Index] = true;
            }
        }
        public int CompareTo(VariableBitArray<T> other)
        {
            if (BitArray.Length == other.BitArray.Length)
            {
                if (Count.CompareTo(other.Count) != 0) return Count.CompareTo(other.Count);
                for (int i = 0; i < BitArray.Length; i++)
                    if (BitArray[i] != other.BitArray[i])
                        if (BitArray[i]) return 1;
                        else return -1;
                return 0;
            }
            else return BitArray.Length.CompareTo(other.BitArray.Length);
        }

        public IEnumerator<Variable<T>> GetEnumerator()
        {
            for (int i = 0; i < BitArray.Length; i++)
                if (BitArray[i])
                    yield return Variables[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override string ToString()
        {
            List<Variable<T>> values = new();
            for (int i = 0; i < BitArray.Length; i++)
                if (BitArray[i])
                    values.Add(Variables[i]);
            return $"[{string.Join(",", values)}]";
        }
    }
}
