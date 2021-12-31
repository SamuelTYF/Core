using Language;
using TestFramework;
namespace Language.Test
{
    public class NFA_Test : ITest
    {
        public NFA_Test()
            : base("NFA_Test", 3)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            Alphabet alphabet = new("01");
            Assemble<Variable<int>> variables = Variable<int>.Create(0, 1, 2, 3);
            bool[] end = { false, false, false, true };
            NFA<int> nfa = new(alphabet, variables, 0, end);
            nfa.SetTransition(0, 0, 0);
            nfa.SetTransition(0, 1, 0);
            nfa.SetTransition(0, 0, 1);
            nfa.SetTransition(0, 1, 2);
            nfa.SetTransition(1, 0, 3);
            nfa.SetTransition(2, 1, 3);
            nfa.SetTransition(3, 0, 3);
            nfa.SetTransition(3, 1, 3);
            UpdateInfo(nfa);
            DFA<VariableBitArray<int>> dfa = nfa.ToDFA();
            UpdateInfo(dfa);
            UpdateInfo(dfa.PrintTable());
            DFA<int> dfaint = dfa.Cast((v) => v.Index);
            UpdateInfo(dfaint);
            UpdateInfo(dfaint.PrintTable());
        }
    }
}
