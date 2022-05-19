namespace Compiler.Parser
{
    public class State
    {
        public Delta Delta;
        public int Index;
        public List<int> Closures;
        public List<State> NextLink;
        public bool End;
        public HashSet<Symbol> Predicts;
        public State(Delta delta,int index)
        {
            Delta = delta;
            Index = index;
            End = delta.Deltas.Length == index;
            Closures = new();
            NextLink = new();
            Predicts = new();
        }
        public Symbol NextSymbol() => Delta.Deltas[Index];
        public override string ToString() => $"{Delta.Start}->{string.Join<Symbol>("", Delta.Deltas[0..Index])}.{string.Join<Symbol>("", Delta.Deltas[Index..])}::::{string.Join("  ", Predicts)}";
    }
}
