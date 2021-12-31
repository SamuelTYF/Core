using Collection;

namespace Language
{
    public class DFATransition<TVariable>
    {
        public Variable<TVariable> From;
        public Terminator Terminator;
        public Variable<TVariable> To;
        public DFATransition(Variable<TVariable> from,Terminator terminator,Variable<TVariable> to)
        {
            From = from;
            Terminator = terminator;
            To = to;
        }
        public override string ToString()
            => $"delta({From},{Terminator})->{To}";
    }
    public class DFATransitionCollection<TVariable>
    {
        public List<DFATransition<TVariable>> List;
        public List<DFATransition<TVariable>>[] Variables;
        public DFATransition<TVariable>[,] Transitions;
        public DFATransitionCollection(int variablecount,int terminatorcount)
        {
            List = new();
            Variables = new List<DFATransition<TVariable>>[variablecount];
            for (int i = 0; i < variablecount; i++)
                Variables[i] = new();
            Transitions = new DFATransition<TVariable>[variablecount, terminatorcount];
        }
        public void Set(Variable<TVariable> from,Terminator terminator, Variable<TVariable> transitions)
        {
            DFATransition<TVariable> transition = new(from, terminator, transitions);
            List.Add(transition);
            Variables[from.Index].Add(transition);
            Transitions[from.Index, terminator.Index] = transition;
        }
        public override string ToString()
            => $"{{{string.Join(",", List)}}}";
    }
}
