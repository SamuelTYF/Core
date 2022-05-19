namespace Compiler.Parser
{
    public class Closure
    {
        public List<State> SuccessStates;
        public List<State> TerminalStates;
        public List<State> VariableStates;
        public int Index;
        public int StateCount;
        public State[][] States;
        public List<State>[] NextTerminalStates;
        public List<State>[] NextVariableStates;
        public Closure(int index)
        {
            Index = index;
            SuccessStates = new();
            TerminalStates = new();
            VariableStates = new();
        }
        public override string ToString()
            =>$"Closure:{Index}\n" +string.Join("\n", SuccessStates.Concat(TerminalStates).Concat(VariableStates));
    }
}
