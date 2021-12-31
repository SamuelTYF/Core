using Collection;

namespace Language
{
    public class NFATransition<TVariable>
    {
        public Variable<TVariable> From;
        public Terminator Terminator;
        public List<Variable<TVariable>> To;
        public bool[] BitArray;
        public NFATransition(int length,Variable<TVariable> from, Terminator terminator)
        {
            From = from;
            Terminator = terminator;
            To = new();
            BitArray=new bool[length];
        }
        public void Add(Variable<TVariable> to)
        {
            if(!BitArray[to.Index])
            {
                BitArray[to.Index] = true;
                To.Add(to);
            }
        }
        public override string ToString()
            => $"delta({From},{Terminator})->{{{string.Join(",", To)}}}";
    }
    public class NFATransitionCollection<TVariable>
    {
        public int VariableCount;
        public List<NFATransition<TVariable>> List;
        public List<NFATransition<TVariable>>[] Variables;
        public NFATransition<TVariable>[,] Transitions;
        public NFATransitionCollection(int variablecount, int terminatorcount)
        {
            VariableCount = variablecount;
               List = new();
            Variables = new List<NFATransition<TVariable>>[variablecount];
            for (int i = 0; i < variablecount; i++)
                Variables[i] = new();
            Transitions = new NFATransition<TVariable>[variablecount, terminatorcount];
        }
        public void Set(Variable<TVariable> from, Terminator terminator, Variable<TVariable> transitions)
        {
            if (Transitions[from.Index, terminator.Index] == null)
            {
                NFATransition<TVariable> transition = new(VariableCount,from, terminator);
                List.Add(transition);
                Variables[from.Index].Add(transition);
                Transitions[from.Index, terminator.Index] = transition;
            }
            Transitions[from.Index, terminator.Index].Add(transitions);
        }
        public override string ToString()
            => $"{{{string.Join(",", List)}}}";
    }
}
