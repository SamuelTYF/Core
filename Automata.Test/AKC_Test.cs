using TestFramework;
namespace Automata.Test
{
    public class AKC_Test : ITest
    {
        public AKC_Test()
            : base("AKC_Test",1)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            AKC.ReadFrom(Properties.Resources.XPath);
            update(1);
        }
    }
}
