namespace Compiler.Parser
{
    public class Delta
    {
        public Symbol Start;
        public Symbol[] Deltas;
        public string? Action;
        public First? First;
        public int Index;
        public Delta(Symbol start, Symbol[] deltas)
        {
            Start = start;
            Deltas = deltas;
        }
        public override string ToString() => $"{Start}->{string.Join<Symbol>("", Deltas)}";
    }
}