namespace Compiler.Tokenizor.Automata
{
    public class ENFA
    {
        public int VariableCount;
        public int Start = 0;
        public int End = 1;
        public int TerminalCount;
        public List<EDelta> Deltas;
        public ENFA(int terminals)
        {
            VariableCount = 2;
            TerminalCount = terminals;
            Deltas = new();
        }
        public int RegisterVariable() => VariableCount++;
        public void InsertDelta(int start, int? terminal, int end) => Deltas.Add(new(start, terminal, end));
        public NFA ToNFA()
        {
            HashSet<int>[] nullclosure = new HashSet<int>[VariableCount];
            HashSet<int>[] nullback = new HashSet<int>[VariableCount];
            bool[,] connect = new bool[VariableCount, VariableCount];
            HashSet<int>[,] deltas = new HashSet<int>[VariableCount, TerminalCount];
            for(int i=0;i<VariableCount;i++)
            {
                nullclosure[i] = new HashSet<int>(new int[] { i });
                nullback[i] = new HashSet<int>(new int[] { i });
                connect[i, i] = true;
                for (int j = 0; j < TerminalCount; j++)
                    deltas[i, j] = new();
            }
            foreach(EDelta delta in Deltas)
            {
                if (delta.Terminal.HasValue) deltas[delta.Start, delta.Terminal.Value].Add(delta.End);
                else if (!connect[delta.Start,delta.End])
                {
                    foreach(int back in nullback[delta.Start])
                        foreach(int forward in nullclosure[delta.End])
                            if (!connect[back,forward])
                            {
                                connect[back,forward] = true;
                                nullclosure[back].Add(forward);
                                nullback[forward].Add(back);
                            }
                }
            }
            bool[] ends = new bool[VariableCount];
            ends[End] = true;
            if (connect[Start, End]) ends[Start] = true;
            NFA nfa = new(VariableCount, TerminalCount, Start, ends);
            for(int start = 0; start < VariableCount; start++)
                for(int terminal=0; terminal < TerminalCount; terminal++)
                    foreach(int i in nullclosure[start])
                        foreach (int j in deltas[i, terminal])
                            foreach (int end in nullclosure[j])
                                nfa.InsertDelta(start, terminal, end);
            return nfa;
        }
        public override string ToString() => $"{string.Join("\n", Deltas)}";
    }
    public class EDelta
    {
        public int Start;
        public int? Terminal;
        public int End;
        public EDelta(int start, int? terminal, int end)
        {
            Start = start;
            Terminal = terminal;
            End = end;
        }
        public override string ToString() => $"δ(S{Start},{(Terminal.HasValue ? Terminal.ToString() : "ϵ")})=S{End}";
    }
}
