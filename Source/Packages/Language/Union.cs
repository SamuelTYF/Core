using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class UnionAssemble<T1,T2>:IAssemble<Union<T1,T2>>
    {
        public IAssemble<T1> Assemble1;
        public IAssemble<T2> Assemble2;
        public UnionAssemble(IAssemble<T1> assemble1,IAssemble<T2> assemble2)
        {
            Assemble1= assemble1;
            Assemble2 = assemble2;
        }
        public Union<T1, T2> this[int index] => throw new NotImplementedException();

        public int Count => Assemble1.Count * Assemble2.Count;

        public IEnumerator<Union<T1, T2>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class Union<T1,T2>
    {
        public T1 Value1;
        public T2 Value2;
        public bool IsT1;
        public Union(T1 value)
        {
            Value1 = value;
            IsT1= true;
        }
        public Union(T2 value)
        {
            Value2 = value;
            IsT1 = false;
        }
        public static implicit operator Union<T1, T2>(T1 value) => new(value);
        public static implicit operator Union<T1, T2>(T2 value) => new(value);
        public override string ToString() => IsT1 ? $"{Value1}" : $"{Value2}";
    }
}
