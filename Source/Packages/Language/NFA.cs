using Collection;
using System.Text;

namespace Language
{
    public class NFA<T>
    {
        public Alphabet Alphabet;
        public IAssemble<Variable<T>> Variables;
        public NFATransitionCollection<T> Transitions;
        public Variable<T> Start;
        public bool[] End;
        public NFA(Alphabet alphabet,IAssemble<Variable<T>> variables, NFATransitionCollection<T> transitions,Variable<T> start,bool[] end)
        {
            Alphabet = alphabet;
            Variables = variables;
            Transitions = transitions;
            Start = start;
            End = end;
        }
        public NFA(Alphabet alphabet,IAssemble<Variable<T>> variables,int start,bool[] end)
        {
            Alphabet = alphabet;
            Variables = variables;
            Transitions = new(Variables.Count, Alphabet.Count);
            Start = Variables[start];
            End = end;
        }
        public void SetTransition(int variable, int terminitor, int to)
            =>Transitions.Set(Variables[variable], Alphabet[terminitor], Variables[to]);
        public DFA<VariableBitArray<T>> ToDFA()
        {
            AVL<VariableBitArray<T>, int> BitArrays = new();
            VariableBitArray<T> start = new(Variables);
            start.Set(Start);
            start.End = true;
            start.End = End[Start.Index];
            List<Variable<VariableBitArray<T>>> variables=new();
            List<(int, int,int)> ts = new();
            Queue<VariableBitArray<T>> queue = new();
            queue.Insert(start);
            variables.Add(new(start,0));
            BitArrays[start] = 0;
            while(queue.Count>0)
            {
                VariableBitArray<T> array = queue.Pop();
                VariableBitArray<T>[] nexts = new VariableBitArray<T>[Alphabet.Count];
                for (int i = 0; i < Alphabet.Count; i++)
                    nexts[i] = new(Variables);
                foreach(Variable<T> variable in array)
                        foreach(NFATransition<T> transition in Transitions.Variables[variable.Index])
                            foreach (Variable<T> v in transition.To)
                            {
                                nexts[transition.Terminator.Index].Set(v);
                                if (End[v.Index]) nexts[transition.Terminator.Index].End = true;
                            }
                int arrayindex = BitArrays[array];
                for(int i=0;i<nexts.Length;i++)
                {
                    VariableBitArray<T> next = nexts[i];
                    if(!BitArrays.ContainsKey(next))
                    {
                        BitArrays[next] = variables.Length;
                        queue.Insert(next);
                        variables.Add(new(next, variables.Length));
                    }
                    ts.Add((arrayindex,i,BitArrays[next]));
                }
            }
            bool[] end = new bool[variables.Length];
            for (int i = 0; i < variables.Length; i++)
                end[i] = variables[i].Name.End;
            DFA<VariableBitArray<T>> dfa = new(Alphabet,new Assemble<Variable<VariableBitArray<T>>>(variables.ToArray()), 0,end);
            foreach((int from,int terminator,int to) in ts)
                dfa.SetTransition(from, terminator, to);
            return dfa;
        }
        public override string ToString()
        {
            List<Variable<T>> ends = new();
            for (int i = 0; i < End.Length; i++)
                if (End[i])
                    ends.Add(Variables[i]);
            return $"({Variables},{Alphabet},{Transitions},{Start},{{{string.Join(",", ends)}}})";
        }
    }
}
