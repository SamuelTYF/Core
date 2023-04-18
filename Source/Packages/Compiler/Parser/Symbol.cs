namespace Compiler.Parser
{
    public class Symbol:IComparable<Symbol>
    {
        public bool IsVariable;
        public bool IsTerminal => !IsVariable;
        public string Name;
        public int Index;
        public First? First;
        public ulong Hash;
        public ulong SHash;
        public string? Type;
        public Symbol(bool isVariable, string name)
        {
            IsVariable = isVariable;
            Name = name;
            Hash= 14695981039346656037;
            for(int i=0;i<name.Length;i++)
            {
                Hash ^= name[i];
                Hash *= 1099511628211;
                Hash *= 1099511628211;
            }
            SHash = 14695981039346656037;
            for (int i = 0; i < name.Length; i++)
            {
                SHash ^= name[i];
                SHash *= 1099511628211;
            }
        }
        public static Symbol Variable(string name) => new(true, name);
        public static Symbol Terminal(string name) => new(false, name);
        public override string ToString()
            => IsVariable ? $"<{Name}>" : $"'{Name}'";
        public int CompareTo(Symbol? other) => Index.CompareTo(other.Index);
    }
}