namespace Compiler.Tokenizor.Automata
{
    public class mNFA
    {
        public int VariableCount;
        public int TerminalCount;
        public int GroupCount;
        public int Start;
        public List<int?> Ends;
        public List<HashSet<int>[]> Deltas;
        public List<string> Errors;
        public mNFA(int terminals,int groups)
        {
            VariableCount = 0;
            TerminalCount = terminals;
            GroupCount = groups;
            Ends = new();
            Deltas = new();
            Start = RegisterVariable(null);
            Errors = new();
        }
        public int RegisterVariable(int? end)
        {
            HashSet<int>[] delta = new HashSet<int>[TerminalCount];
            for (int i = 0; i < TerminalCount; i++)
                delta[i] = new();
            Deltas.Add(delta);
            Ends.Add(end);
            return VariableCount++;
        }
        public void InsertDelta(int start,int terminal,int end)
            => Deltas[start][terminal].Add(end);
        public mDFA TomDFA()
        {
            mDFA dfa = new(TerminalCount,GroupCount);
            Variable_Closure startclosure = new(Start);
            Dictionary<string, int> variables = new();
            variables[startclosure.ToString()] = startclosure.Index = dfa.RegisterVariable(Ends[Start]);
            Queue<Variable_Closure> queue = new();
            queue.Enqueue(startclosure);
            while (queue.Count > 0)
            {
                Variable_Closure closure = queue.Dequeue();
                for (int terminal = 0; terminal < TerminalCount; terminal++)
                {
                    Variable_Closure next = new();
                    int? ended = null;
                    foreach (int start in closure.Variables)
                        foreach (int end in Deltas[start][terminal])
                        {
                            next.Variables.Add(end);
                            if (Ends[end].HasValue)
                            {
                                if (ended.HasValue && Ends[end].Value != ended.Value)
                                    Errors.Add($"mNFA Error: State Can't Execute Different Actions");
                                else ended = Ends[end].Value;
                            }
                        }
                    if (next.Variables.Count > 0)
                    {
                        string name = next.ToString();
                        if (!variables.ContainsKey(name))
                        {
                            variables[name] = next.Index = dfa.RegisterVariable(ended);
                            queue.Enqueue(next);
                        }
                        dfa.InsertDelta(closure.Index, terminal, variables[name]);
                    }
                }
            }
            return dfa;
        }
    }
}
