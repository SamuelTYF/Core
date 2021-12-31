using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Language
{
    public class Closure<T>:IAssemble<ClosureEntity<T>>
    {
        public IAssemble<T> Assemble;
        public ClosureEntity<T> this[int index] => throw new System.NotImplementedException();
        public int Count => throw new System.NotImplementedException();
        public IEnumerator<ClosureEntity<T>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClosureEntity<T>
    {
        public T[] Values;
        public int Count => Values.Length;
        public ClosureEntity(params T[] values) => Values = values;
        public override string ToString() =>Count==0? "ε" : string.Join("",Values);
    }
}
