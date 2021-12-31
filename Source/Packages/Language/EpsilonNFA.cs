using Collection;

namespace Language
{
    public class EpsilonNFA<T>
    {
        public Alphabet Alphabet;
        public IAssemble<Variable<T>> Variables;
        public EpsilonNFATransitionCollection<T> Transitions;
        public Variable<T> Start;
        public bool[] End;
        public bool[,] Connected;
        public List<int>[] Closures;
        public List<int>[] InverseClosures;
        public EpsilonNFA(Alphabet alphabet, IAssemble<Variable<T>> variables, int start, bool[] end)
        {
            Alphabet = alphabet;
            Variables = variables;
            Transitions = new(Variables.Count, Alphabet.Count);
            Start = Variables[start];
            End = end;
            Connected=new bool[Variables.Count,Variables.Count];
            Closures=new List<int>[Variables.Count];
            InverseClosures=new List<int>[Variables.Count];
            for (int i = 0; i < Variables.Count; i++)
            {
                Connected[i,i] = true;
                Closures[i] = new();
                InverseClosures[i] = new();
                Closures[i].Add(i);
                InverseClosures[i].Add(i);
            }
        }
        public void SetTransition(int variable, int terminitor, int to)
            =>Transitions.Set(Variables[variable], Alphabet[terminitor], Variables[to]);
        public void SetEpsilonTransition(int variable, int to)
        {
            Transitions.Set(Variables[variable],null, Variables[to]);
            if(!Connected[variable,to])
            {
                foreach (int i in InverseClosures[variable])
                    foreach (int v in Closures[to])
                        if (!Connected[i, v])
                        {
                            Connected[i, v] = true;
                            Closures[i].Add(v);
                            InverseClosures[v].Add(i);
                        }
            }
        }
        public NFA<T> ToNFA()
        {
            bool[] end=new bool[End.Length];
            bool Fq0 = false;
            for (int i = 0; i < End.Length; i++)
            {
                end[i] = End[i];
                if (End[i] && Connected[Start.Index, i])
                    Fq0 = true;
            }
            if (Fq0) end[Start.Index] = true;
            NFA<T> nfa = new(Alphabet,Variables,Start.Index,End);
            for (int from = 0; from < Variables.Count; from++)
                for(int t=0;t<Alphabet.Count;t++)
                {
                    if(Transitions.Transitions[from, t]!=null)
                        foreach (Variable<T> v in Transitions.Transitions[from, t].To)
                            foreach (int to in Closures[v.Index])
                                nfa.SetTransition(from, t, to);
                    foreach (int v in Closures[from])
                        if (Transitions.Transitions[v, t] != null)
                            foreach (Variable<T> to in Transitions.Transitions[v,t].To)
                                nfa.SetTransition(from, t, to.Index);
                }
            return nfa;
        }
    }
}
