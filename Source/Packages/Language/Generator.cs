using System.Collections.Generic;

namespace Language
{
    public class Generator
    {
        public static IEnumerable<List<Terminator>> Create(Alphabet alphabet,int length)
        {
            if (length == 0)
                foreach (Terminator terminator in alphabet)
                {
                    List<Terminator> t = new();
                    t.Add(terminator);
                    yield return t;
                }
            else {
                foreach (List<Terminator> t in Create(alphabet,length-1))
                {
                    if (t.Count <= length)
                        t.Add(null);
                    foreach(Terminator terminator in alphabet)
                    {
                        t[length]=terminator;
                        yield return t;
                    }
                }
            }
        }
    }
}
