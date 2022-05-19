namespace Compiler.Parser
{
    public class Symbol:IComparable<Symbol>
    {
        public bool IsVariable;
        public bool IsTerminal => !IsVariable;
        public string Name;
        public int Index;
        public First? First;
        public Symbol(bool isVariable, string name)
        {
            IsVariable = isVariable;
            Name = name;
        }
        public static Symbol Variable(string name) => new(true, name);
        public static Symbol Terminal(string name) => new(false, name);
        public override string ToString()
            => IsVariable ? $"<{Name}>" : $"'{Name}'";
        public int CompareTo(Symbol? other) => Index.CompareTo(other.Index);
    }
}