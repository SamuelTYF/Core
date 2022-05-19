namespace Compiler.Tokenizor.Automata
{
    public class mDFA
    {
        public int VariableCount;
        public int TerminalCount;
        public int GroupCount;
        public List<int?[]> Deltas;
        public List<int?> Ends;
        public mDFA(int terminals,int groups)
        {
            VariableCount = 0;
            TerminalCount = terminals;
            GroupCount = groups;
            Deltas = new();
            Ends = new();
        }
        public int RegisterVariable(int? end)
        {
            Deltas.Add(new int?[TerminalCount]);
            Ends.Add(end);
            return VariableCount++;
        }
        public void InsertDelta(int start, int terminal, int end) => Deltas[start][terminal] = end;
        public mDFA Simplify()
        {
            List<int>[] ends = new List<int>[GroupCount+1];
            for (int i = 0; i <= GroupCount; i++)
                ends[i] = new();
            for (int i = 0; i < VariableCount; i++)
                if (Ends[i].HasValue) ends[Ends[i].Value].Add(i);
                else ends[GroupCount].Add(i);

            bool[,] same = new bool[VariableCount, VariableCount];
            for (int i = 1; i <= GroupCount; i++)
                for (int j = 0; j < i; j++)
                    foreach (int a in ends[i])
                        foreach (int b in ends[j])
                            same[a, b] = same[b, a] = true;

            Link[,] links = new Link[VariableCount, VariableCount];
            for (int i = 0; i < VariableCount; i++)
                for (int j = 0; j < VariableCount; j++)
                    links[i, j] = new(i, j);
            foreach(List<int> end in ends)
                foreach (int i in end)
                    foreach (int j in end)
                        if (i != j)
                        {
                            bool s = false;
                            for (int k = 0; k < TerminalCount; k++)
                            {
                                int? di = Deltas[i][k];
                                int? dj = Deltas[j][k];
                                if (!di.HasValue && !dj.HasValue) continue;
                                if (!di.HasValue || !dj.HasValue || same[di.Value, dj.Value])
                                {
                                    s = true;
                                    break;
                                }
                            }
                            if (s)
                            {
                                Link link = links[i, j];
                                while (link != null)
                                {
                                    same[link.X, link.Y] = same[link.Y, link.X] = true;
                                    link = link.Last;
                                }
                            }
                            else
                            {
                                for (int k = 0; k < TerminalCount; k++)
                                {
                                    int? di = Deltas[i][k];
                                    int? dj = Deltas[j][k];
                                    if (!di.HasValue && !dj.HasValue) continue;
                                    if (di.HasValue && dj.HasValue && di.Value != dj.Value)
                                    {
                                        if (i == di.Value && j == dj.Value) continue;
                                        if (i == dj.Value && j == di.Value) continue;
                                        links[di.Value, dj.Value].Last = links[i, j];
                                        links[dj.Value, di.Value].Last = links[j, i];
                                    }
                                }
                            }
                        }
            mDFA dfa = new(TerminalCount,GroupCount);
            int[] r = new int[VariableCount];
            Array.Fill(r, -1);
            for (int i = 0; i < VariableCount; i++)
                if (r[i] < 0)
                {
                    int label = dfa.RegisterVariable(Ends[i]);
                    for (int j = 0; j < VariableCount; j++)
                        if (!same[i, j])
                            r[j] = label;
                }
            for (int start = 0; start < VariableCount; start++)
                for (int terminal = 0; terminal < TerminalCount; terminal++)
                    if (Deltas[start][terminal].HasValue)
                        dfa.InsertDelta(r[start], terminal, r[Deltas[start][terminal].Value]);
            return dfa;
        }
        public override string ToString()
        {
            List<string> deltas = new();
            for (int i = 0; i < VariableCount; i++)
                for (int j = 0; j < TerminalCount; j++)
                    if (Deltas[i][j].HasValue)
                        deltas.Add($"δ(S{i},{j})=S{Deltas[i][j]}");
            return $"DFA\n{string.Join("\n", deltas)}";
        }
    }
}