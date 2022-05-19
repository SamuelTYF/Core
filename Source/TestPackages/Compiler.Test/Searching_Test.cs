using Compiler.CSharp.Searching;
using TestFramework;

namespace Compiler.Test
{
    public class Searching_Test : ITest
    {
        public Searching_Test()
            : base("Searching_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            SearchingNode_Root root = new();
            root.LoadAssembly(typeof(string).Assembly);
        }
    }
}
