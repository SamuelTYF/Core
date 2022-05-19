using Compiler.Parser;
using TestFramework;
namespace Compiler.Test
{
    public class Test_LALR_Test : ITest
    {
        public Test_LALR_Test()
            :base("Test_LALR_Test", 3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            LALR lalr = new();
            lalr.Register(Properties.Resources.Test_LALR);
            UpdateInfo(string.Join("\n", lalr.Errors));
            Ensure.Equal(lalr.Errors.Count, 0);
            update(1);
            lalr.ComputeFirst();
            UpdateInfo(lalr.PrintFirst());
            update(2);
            lalr.CreateClosures();
            foreach (Closure closure in lalr.Closures)
                UpdateInfo(closure);
            UpdateInfo(string.Join("\n", lalr.Errors));
            Ensure.Equal(lalr.Errors.Count, 0);
            update(3);
        }
    }
}
