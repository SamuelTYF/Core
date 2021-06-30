using TestFramework;
namespace NumericalAnalysis.Test
{
    public class FactorHelper_Test:ITest
    {
        public FactorHelper_Test()
            :base("FactorHelper_Test",6)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Polynomial p = new(-1,0,0,0,0,1);
            Ensure.Equal(p[1], 0);
            update(1);
            Complex[] cs = FactorHelper.GetRoots(p.P,0.0000001,10000);
            for (int i = 0; i < cs.Length; i++)
            {
                UpdateInfo(cs[i]);
                Ensure.DoubleEqual(p[cs[i]].Norm(), 0);
                update(i + 2);
            }
        }
    }
}
