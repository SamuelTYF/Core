using Language;
using TestFramework;
namespace Language.Test
{
    public class EpsilonNFA_Test : ITest
    {
        public EpsilonNFA_Test()
            : base("EpsilonNFA_Test", 3)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            Alphabet alphabet = new("012");
            Assemble<Variable<int>> variables = Variable<int>.Create(0, 1, 2);
            bool[] end = { false, false, true };
            EpsilonNFA<int> enfa = new(alphabet, variables, 0, end);
            enfa.SetTransition(0, 0, 0);
            enfa.SetTransition(1, 1, 1);
            enfa.SetTransition(2, 2, 2);
            enfa.SetEpsilonTransition(0, 1);
            enfa.SetEpsilonTransition(1, 2);
            UpdateInfo(enfa);
            NFA<int> nfa = enfa.ToNFA();
            UpdateInfo(nfa);
        }
    }
}
