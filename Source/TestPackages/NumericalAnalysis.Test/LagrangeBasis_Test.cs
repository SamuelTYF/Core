using System;
using NumericalAnalysis.Interpolating;
using TestFramework;
namespace NumericalAnalysis.Test
{
    public class LagrangeBasis_Test:ITest
    {
        public LagrangeBasis_Test()
            :base("LagrangeBasis_Test",11)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            double[] xs = new double[10];
            Random _R = new(DateTime.Now.Millisecond);
            for (int i = 0; i < xs.Length; i++)
                xs[i] = i;
            LagrangeBasis bs = new(xs);
            bs.ComputePi();
            update(1);
            for(int i=0;i<xs.Length;i++)
            {
                Polynomial p = bs.L(i);
                UpdateInfo(p);
                for(int j=0;j<xs.Length;j++)
                Ensure.DoubleEqual(p[xs[j]],i==j?1:0,0.001);
                update(i + 2);
            }
        }
    }
}
