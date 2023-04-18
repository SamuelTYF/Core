using Operations.LinearProgram.Simplex;
using TestFramework;
namespace Operations.Test
{
    public class SSF_Test:ITest
    {
        public SSF_Test()
            :base("SSF_Test",2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            SSF ssf = new(4, 6, 2, 10, 8);
            ssf.RegisterRule(0, 5, 6, -4, -4, 20);
            ssf.RegisterRule(1, 3, -3, 2, 8, 25);
            ssf.RegisterRule(2, 4, -2, 1, 3, 10);
            UpdateInfo(ssf);
            while (!ssf.Run())
                UpdateInfo(ssf);
            UpdateInfo(ssf);
        }
    }
}
