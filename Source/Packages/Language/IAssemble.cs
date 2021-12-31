using System.Collections;
using System.Collections.Generic;
namespace Language
{
    public interface IAssemble<T>:IEnumerable<T>
    {
        int Count { get; }
        T this[int index] { get; }
    }
    public class Assemble<T> : IAssemble<T>
    {
        public T[] Values;
        public int Count => Values.Length;
        public T this[int index]=>Values[index];
        public Assemble(T[] values) => Values = values;
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T value in Values)
                yield return value;
        }
        IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
        public override string ToString()
            => $"{{{string.Join(",", Values)}}}";
    }
}
