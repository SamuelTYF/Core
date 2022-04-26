using Language;
using System.Collections.Generic;
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
            Alphabet alphabet = new("012");
            Assemble<Variable<char>> variables = Variable<char>.Create('A','B','C','D','E','S');
            bool[] end = { false, false, false, false, false, true };
            NFA<char> nfa = new(alphabet, variables, 0, end);
            nfa.SetTransition(0, 0, 1); nfa.SetTransition(0, 1, 1); nfa.SetTransition(0, 2, 1);
            nfa.SetTransition(1, 0, 2); nfa.SetTransition(1, 1, 1); nfa.SetTransition(1, 2, 1);
            nfa.SetTransition(2, 0, 4); nfa.SetTransition(2, 1, 3); nfa.SetTransition(2, 2, 3);
            nfa.SetTransition(2, 0, 5); nfa.SetTransition(2, 1, 5); nfa.SetTransition(2, 2, 5);
            nfa.SetTransition(3, 0, 2);nfa.SetTransition(3, 1, 1);nfa.SetTransition(3, 2, 1);
            nfa.SetTransition(4, 0, 4);nfa.SetTransition(4, 1, 3);nfa.SetTransition(4, 2, 3);
            nfa.SetTransition(4, 0, 5);nfa.SetTransition(4, 1, 5);nfa.SetTransition(4, 2, 5);
            UpdateInfo(nfa);
            DFA<VariableBitArray<char>> dfa = nfa.ToDFA();
            UpdateInfo(dfa);
            UpdateInfo(dfa.PrintTable());
            DFA<int> dfaint = dfa.Cast((v) => v.Index);
            UpdateInfo(dfaint);
            UpdateInfo(dfaint.PrintTable());
            DFA<int> r = dfaint.Simplify().Cast(v=>v.Index);
            UpdateInfo(r);
            UpdateInfo(r.PrintTable());
        }
    }
}
