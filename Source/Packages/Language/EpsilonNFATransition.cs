using Collection;

namespace Language
{
    public class EpsilonNFATransition<TVariable>
    {
        public Variable<TVariable> From;
        public Terminator Terminator;
        public List<Variable<TVariable>> To;
        public EpsilonNFATransition(Variable<TVariable> from, Terminator terminator)
        {
            From = from;
            Terminator = terminator;
            To = new();
        }
        public void Add(Variable<TVariable> to) => To.Add(to);
        public override string ToString()
            => $"delta({From},{Terminator})->{{{string.Join(",", To)}}}";
    }
    public class EpsilonNFATransitionCollection<TVariable>
    {
        public List<EpsilonNFATransition<TVariable>> List;
        public List<EpsilonNFATransition<TVariable>>[] Variables;
        public EpsilonNFATransition<TVariable>[,] Transitions;
        public EpsilonNFATransition<TVariable>[] EpsilonTransitions;
        public EpsilonNFATransitionCollection(int variablecount, int terminatorcount)
        {
            List = new();
            Variables = new List<EpsilonNFATransition<TVariable>>[variablecount];
            for (int i = 0; i < variablecount; i++)
                Variables[i] = new();
            Transitions = new EpsilonNFATransition<TVariable>[variablecount, terminatorcount];
            EpsilonTransitions = new EpsilonNFATransition<TVariable>[variablecount];
        }
        public void Set(Variable<TVariable> from, Terminator terminator, Variable<TVariable> transitions)
        {
            if(terminator==null)
            {
                if (EpsilonTransitions[from.Index] == null)
                {
                    EpsilonNFATransition<TVariable> transition = new(from, null);
                    List.Add(transition);
                    Variables[from.Index].Add(transition);
                    EpsilonTransitions[from.Index] = transition;
                }
                EpsilonTransitions[from.Index].Add(transitions);
            }
            else
            {
                if (Transitions[from.Index, terminator.Index] == null)
                {
                    EpsilonNFATransition<TVariable> transition = new(from, terminator);
                    List.Add(transition);
                    Variables[from.Index].Add(transition);
                    Transitions[from.Index, terminator.Index] = transition;
                }
                Transitions[from.Index, terminator.Index].Add(transitions);
            }
        }
        public override string ToString()
            => $"{{{string.Join(",", List)}}}";
    }
}
