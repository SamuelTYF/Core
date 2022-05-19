namespace Compiler.Parser
{
    public class First
    {
        public HashSet<Symbol> Firsts;
        public bool IsEpsilon;
        public HashSet<Symbol> FirstVariables;
        public First()
        {
            Firsts = new();
            IsEpsilon = false;
            FirstVariables = new();
        }
        public bool Add(Symbol terminal)
        {
            int count = Firsts.Count;
            Firsts.Add(terminal);
            return Firsts.Count > count;
        }
        public bool AddVariable(Symbol variable)
        {
            int count = FirstVariables.Count;
            FirstVariables.Add(variable);
            return FirstVariables.Count > count;
        }
        public bool AddRange(HashSet<Symbol> set)
        {
            int count = Firsts.Count;
            Firsts.UnionWith(set);
            return Firsts.Count > count;
        }
        public bool AddVariableRange(HashSet<Symbol> set)
        {
            int count = FirstVariables.Count;
            FirstVariables.UnionWith(set);
            return FirstVariables.Count > count;
        }
    }
}
