namespace Compiler.Tokenizor.Automata
{
    public class NFA
    {
        public int VariableCount;
        public int TerminalCount;
        public int Start;
        public bool[] Ends;
        public HashSet<int>[,] Deltas;
        public NFA(int variables, int terminals, int start, bool[] end)
        {
            VariableCount = variables;
            TerminalCount = terminals;
            Start = start;
            Ends = end;
            Deltas = new HashSet<int>[variables, terminals];
            for(int i=0;i<variables;i++)
                for(int j=0;j<terminals;j++)
                    Deltas[i,j] = new();
        }
        public DFA ToDFA()
        {
            DFA dfa = new(TerminalCount);
            Variable_Closure startclosure = new(Start);
            Dictionary<string, int> variables = new();
            variables[startclosure.ToString()] = startclosure.Index = dfa.RegisterVariable(Ends[Start]);
            Queue<Variable_Closure> queue = new();
            queue.Enqueue(startclosure);
            while(queue.Count>0)
            {
                Variable_Closure closure = queue.Dequeue();
                for (int terminal = 0; terminal < TerminalCount; terminal++)
                {
                    Variable_Closure next = new();
                    bool ended = false;
                    foreach (int start in closure.Variables)
                        foreach(int end in Deltas[start,terminal])
                        {
                            next.Variables.Add(end);
                            if (Ends[end]) ended = true;
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
        public void InsertDelta(int start, int terminal, int end) => Deltas[start, terminal].Add(end);
        public override string ToString()
        {
            List<string> deltas = new();
            for (int i = 0; i < VariableCount; i++)
                for (int j = 0; j < TerminalCount; j++)
                    if (Deltas[i,j].Count>0)
                        deltas.Add($"δ(S{i},{j})=[{string.Join(",", Array.ConvertAll(Deltas[i,j].ToArray(),index=>$"S{index}"))}]");
            return $"NFA\n{string.Join("\n", deltas)}";
        }
    }
    public class Variable_Closure
    {
        public HashSet<int> Variables;
        public int Index;
        public Variable_Closure(params int[] variables) => Variables = new(variables);
        public override string ToString() 
        {
            int[] variables = Variables.ToArray();
            Array.Sort(variables);
            return $"[{string.Join(",", variables)}]";
        }
    }
}