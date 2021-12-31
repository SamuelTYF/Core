using System.Collections;
using System.Collections.Generic;

namespace Language
{
    public class Alphabet: IAssemble<Terminator>
    {
        public Terminator[] Terminators;
        public int Count => Terminators.Length;
        public Terminator this[int index]=>Terminators[index];
        public Alphabet(string values)
        {
            Terminators=new Terminator[values.Length];
            for (int i = 0; i < values.Length; i++)
                Terminators[i] = new(values[i],i);
        }
        public IEnumerator<Terminator> GetEnumerator()
        {
            foreach(Terminator terminator in Terminators)
                yield return terminator;
        }
        IEnumerator IEnumerable.GetEnumerator() => Terminators.GetEnumerator();
        public override string ToString() => $"{{{string.Join<Terminator>(",", Terminators)}}}";
    }
}
