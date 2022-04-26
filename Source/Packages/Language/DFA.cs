using Collection;
using System;

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
        public class SimplifyLink
        {
            public int X;
            public int Y;
            public SimplifyLink Last;
            public SimplifyLink(int x, int y)
            {
                X = x;
                Y = y;
                Last = null;
            }
        }
        public DFA<Variable<T>[]> Simplify()
        {
            List<int> ends = new();
            List<int> nends = new();
            for (int i = 0; i < End.Length; i++)
                if (End[i]) ends.Add(i);
                else nends.Add(i);
            bool[,] same = new bool[Variables.Count, Variables.Count];
            foreach (int a in ends)
                foreach(int b in nends)
                    same[a, b]=same[b,a] = true;
            SimplifyLink[,] links=new SimplifyLink[Variables.Count,Variables.Count];
            for (int i = 0; i < Variables.Count; i++)
                for (int j = 0; j < Variables.Count; j++)
                    links[i, j] = new(i, j);
            foreach (int i in ends)
                foreach (int j in ends)
                    if (i != j)
                    {
                        bool s = false;
                        for (int k = 0; k < Alphabet.Count; k++)
                        {
                            DFATransition<T> di = Transitions.Transitions[i, k];
                            DFATransition<T> dj = Transitions.Transitions[j, k];
                            if (di == null && dj == null) continue;
                            if (di == null || dj == null || same[di.To.Index, dj.To.Index])
                            {
                                s = true;
                                break;
                            }
                        }
                        if (s)
                        {
                            SimplifyLink link = links[i, j];
                            while (link != null)
                            {
                                same[link.X, link.Y] = true;
                                same[link.Y, link.X] = true;
                                link = link.Last;
                            }
                        }
                        else
                        {
                            for (int k = 0; k < Alphabet.Count; k++)
                            {
                                DFATransition<T> di = Transitions.Transitions[i, k];
                                DFATransition<T> dj = Transitions.Transitions[j, k];
                                if (di != null && dj != null && di.To.Index != dj.To.Index)
                                {
                                    if (i == di.To.Index && j == dj.To.Index) continue;
                                    if (i == dj.To.Index && j == di.To.Index) continue;
                                    links[di.To.Index, dj.To.Index].Last = links[i, j];
                                    links[dj.To.Index, di.To.Index].Last = links[j, i];
                                }
                            }
                        }
                    }
            foreach (int i in nends)
                foreach(int j in nends)
                    if(i!=j)
                    {
                        bool s = false;
                        for(int k=0;k<Alphabet.Count;k++)
                        {
                            DFATransition<T> di = Transitions.Transitions[i, k];
                            DFATransition<T> dj = Transitions.Transitions[j, k];
                            if (di == null && dj == null) continue;
                            if (di == null || dj == null||same[di.To.Index,dj.To.Index])
                            {
                                s = true;
                                break;
                            }
                        }
                        if(s)
                        {
                            SimplifyLink link = links[i, j];
                            while(link!=null)
                            {
                                same[link.X, link.Y] = true;
                                same[link.Y, link.X] = true;
                                link = link.Last;
                            }
                        }
                        else
                        {
                            for (int k = 0; k < Alphabet.Count; k++)
                            {
                                DFATransition<T> di = Transitions.Transitions[i, k];
                                DFATransition<T> dj = Transitions.Transitions[j, k];
                                if (di != null && dj != null&&di.To.Index!=dj.To.Index)
                                {
                                    if (i == di.To.Index && j == dj.To.Index) continue;
                                    if(i==dj.To.Index&&j==di.To.Index) continue;
                                    links[di.To.Index, dj.To.Index].Last = links[i, j];
                                    links[dj.To.Index, di.To.Index].Last = links[j, i];
                                }
                            }
                        }
                    }
            int[] r = new int[Variables.Count];
            Array.Fill(r, -1);
            int rl = 0;
            List<Variable<T>[]> result = new();
            List<bool> rends = new();
            for (int i = 0; i < Variables.Count; i++)
                if (r[i] < 0)
                {
                    List<Variable<T>> temp = new();
                    for (int j = 0; j < Variables.Count; j++)
                        if (!same[i, j])
                        {
                            r[j] = rl;
                            temp.Add(Variables[j]);
                        }
                    result.Add(temp.ToArray());
                    rends.Add(End[i]);
                    rl++;
                }

            Assemble<Variable<Variable<T>[]>> rv = Variable<Variable<T>[]>.Create(result.ToArray());
            DFA<Variable<T>[]> rdfa = new(Alphabet,rv,r[Start.Index],rends.ToArray());
            foreach(DFATransition<T> delta in Transitions.List)
            {
                int from = r[delta.From.Index];
                int to = r[delta.To.Index];
                if (rdfa.Transitions.Transitions[from, delta.Terminator.Index] == null)
                    rdfa.SetTransition(from, delta.Terminator.Index, to);
            }
            return rdfa;
        }
        public bool Test(Terminator[] terminators)
        {
            Variable<T> s = Start;
            foreach(Terminator terminator in terminators)
            {
                var t = Transitions.Transitions[s.Index, terminator.Index];
                if (t is null) return false;
                s = t.To;
            }
            return End[s.Index];
        }
    }
    public delegate TResult DFACast<T, TResult>(Variable<T> value);
}
