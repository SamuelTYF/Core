using Collection;

namespace Language
{
    public class CFGTransition<T>
    {
        public Variable<T> From;
        public ClosureEntity<Union<Variable<T>, Terminator>> To;
        public CFGTransition(Variable<T> from, ClosureEntity<Union<Variable<T>, Terminator>> to)
        {
            From = from;
            To = to;
        }
    }
    public class CFGTransitionCollection<T>
    {
        public List<CFGTransition<T>> List;
        public List<CFGTransition<T>>[] Variables;
        public CFGTransitionCollection(int variablecount)
        {
            List = new();
            Variables = new List<CFGTransition<T>>[variablecount];
            for (int i = 0; i < variablecount; i++)
                Variables[i] = new();
        }
        public void SetTransition(Variable<T> variable, ClosureEntity<Union<Variable<T>, Terminator>> to)
        {
            CFGTransition<T> transition = new(variable, to);
            Variables[variable.Index].Add(transition);
            List.Add(transition);
        }
    }
}
