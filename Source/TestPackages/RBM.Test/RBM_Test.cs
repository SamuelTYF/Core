using System.Collections.Generic;
using TestFramework;

namespace RBM.Test
{
    public class RBM_Test:ITest
    {
        public List<double[]> S;
        public RBM_Test()
            :base("RBM_Test",1000)
        {
            S = new();
            S.Add(new double[] { 0, 0, 0 });
            S.Add(new double[] { 0, 1, 1 });
            S.Add(new double[] { 1, 0, 1 });
            S.Add(new double[] { 1, 1, 1 });
        }
        public override void Run(UpdateTaskProgress update)
        {
            double[][] s = S.ToArray();
            RBM rbm = new(3, 2);
            rbm.RandomInital();
            for(int i=1;i<=10;i++)
            {
                rbm.Learn(s, 1, 0.1);
                update(i);
                UpdateInfo(rbm.GetLogP(s));
            }
            string r = "";
            for(int i=0;i<s.Length;i++)
                r+=$"{i}\n{string.Join(",", rbm.GetH(s[i]))}\n";
            UpdateInfo(r);
        }
    }
}
