using Collection;

namespace Language
{
    public class DFA<T>
    {
        public Alphabet Alphabet;
        public IAssemble<Variable<T>> Variables;
        public DFATransitionCollection<T> Transitions;
        public Variable<T> Start;
        public bool[] End;
        public DFA(Alphabet alphabet,IAssemble<Variable<T>> variables, DFATransitionCollection<T> transitions,Variable<T> start,bool[] end)
        {
            Alphabet = alphabet;
            Variables = variables;
            Transitions = transitions;
            Start = start;
            End = end;
        }
        public DFA(Alphabet alphabet, IAssemble<Variable<T>> variables, int start,bool[] end)
        {
            Alphabet = alphabet;
            Variables= variables;
            Transitions = new(variables.Count,alphabet.Count);
            Start = variables[start];
            End = end;
        }
        public void SetTransition(int variable,int terminitor,int to)
            =>Transitions.Set(Variables[variable], Alphabet[terminitor], Variables[to]);
        public void SetEnd(Variable<T> variable) => End[variable.Index] = true;
        public override string ToString()
        {
            List<Variable<T>> ends = new();
            for (int i = 0; i < End.Length; i++)
                if (End[i])
                    ends.Add(Variables[i]);
            return $"({Variables},{Alphabet},{Transitions},{Start},{{{string.Join(",", ends)}}})";
        }
        public string PrintTableRow(int index)
        {
            string s= $"{(End[index] ? "End" : "")}\t{Variables[index]}";
            for (int i = 0; i < Alphabet.Count; i++)
                if (Transitions.Transitions[index, i] == null)
                    s += "\t";
                else s += $"\t{Transitions.Transitions[index, i].To}";
            return s+"\n";
        }
        public string PrintTable()
        {
            string s = $"State\tVariable\t{string.Join("\t",Alphabet)}\n";
            for (int i = 0; i < Variables.Count; i++)
                s += PrintTableRow(i);
            return s;
        }
        public DFA<TResult> Cast<TResult>(DFACast<T,TResult> cast)
        {
            Variable<TResult>[] variables = new Variable<TResult>[Variables.Count];
            for (int i = 0; i < Variables.Count; i++)
                variables[i] = new Variable<TResult>(cast(Variables[i]), i);
            DFA<TResult> dfa = new(Alphabet, new Assemble<Variable<TResult>>(variables), Start.Index, End);
            for (int i = 0; i < variables.Length; i++)
                foreach (DFATransition<T> transition in Transitions.Variables[i])
                    dfa.SetTransition(i, transition.Terminator.Index, transition.To.Index);
            return dfa;
        }
    }
    public delegate TResult DFACast<T, TResult>(Variable<T> value);
}
