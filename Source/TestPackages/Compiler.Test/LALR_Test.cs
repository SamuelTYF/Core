using Compiler.Parser;
using TestFramework;
namespace Compiler.Test
{
    public class LALR_Test : ITest
    {
        public LALR_Test()
            :base("LALR_Test", 3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            LALR lalr = new();
            lalr.Register(Properties.Resources.CSharp_LALR);
            UpdateInfo(string.Join("\n", lalr.Errors));
            Ensure.Equal(lalr.Errors.Count, 0);
            update(1);
            lalr.ComputeFirst();
            UpdateInfo(lalr.PrintFirst());
            update(2);
            lalr.CreateClosures();
            Ensure.Equal(lalr.Errors.Count, 0);
            foreach (Closure closure in lalr.Closures)
                UpdateInfo(closure);
            update(3);
        }
    }
}
