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
            SSF ssf = new(2, 3, 7, 15);
            ssf.RegisterRule(0, 1, 1, 6);
            ssf.RegisterRule(1, 1, 2, 8);
            ssf.RegisterRule(2, 0, 1, 3);
            UpdateInfo(ssf);
            while(!ssf.Run())
                UpdateInfo(ssf);
            UpdateInfo(ssf);
        }
    }
}
