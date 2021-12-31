using Language;
using TestFramework;
namespace Language.Test
{
    public class CFG_Test : ITest
    {
        public CFG_Test()
            : base("CFG_Test", 3)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            Alphabet alphabet = new("ac");
            Assemble<Variable<char>> variables = Variable<char>.Create("SABCD".ToCharArray());
            CFG<char> cfg = new(alphabet, variables, 0);
            cfg.SetTransition(0,1,2,4,3);
            cfg.SetTransition(1,2,4);
            cfg.SetTransition(1,-1,-1);
            cfg.SetTransition(1);
            cfg.SetTransition(2,-1,2);
            cfg.SetTransition(2,-1);
            cfg.SetTransition(3,4,3);
            cfg.SetTransition(3,-2);
            cfg.SetTransition(3);
            cfg.SetTransition(4);
            UpdateInfo(cfg);
            UpdateInfo(string.Join(",", cfg.FindEmptyVariable()));
        }
    }
}
