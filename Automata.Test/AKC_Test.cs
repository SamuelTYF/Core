using Automata;
using TestFramework;
namespace Automata.Test
{
    public class AKC_Test:ITest
    {
        public AKC_Test() 
            : base("AKC_Test") 
        { 
        }
        public override void Run()
        {
            AutomataInstance instance = AKC.ReadFrom(Properties.Resources.XPath);
        }
    }
}
