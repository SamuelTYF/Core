using Language;
using TestFramework;
namespace Language.Test
{
    public class DFA_Test : ITest
    {
        public DFA_Test()
            : base("DFA_Test", 3)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            Alphabet alphabet = new("01");
            Assemble<Variable<int>> variables = Variable<int>.Create(0, 1, 2, 3, 4, 5);
            bool[] end = { false, false, true, true,false,false };
            DFA<int> dfa = new(alphabet, variables, 0, end);
            dfa.SetTransition(0, 0, 1);
            dfa.SetTransition(0, 1, 2);
            dfa.SetTransition(1, 0, 0);
            dfa.SetTransition(1, 1, 3);
            dfa.SetTransition(2, 0, 3);
            dfa.SetTransition(2, 1, 4);
            dfa.SetTransition(3, 0, 2);
            dfa.SetTransition(3, 1, 5);
            dfa.SetTransition(4, 0, 5);
            dfa.SetTransition(4, 1, 2);
            dfa.SetTransition(5, 0, 4);
            dfa.SetTransition(5, 1, 3);
            UpdateInfo(dfa.PrintTable());
            update(1);
            DFA<Variable<int>[]> temp = dfa.Simplify();
            UpdateInfo(temp.PrintTable());
            update(2);
            DFA<int> r=temp.Cast((v) => v.Index);
            UpdateInfo(r.PrintTable());
            update(3);
        }
    }
}
