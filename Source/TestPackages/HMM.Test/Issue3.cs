using TestFramework;
namespace HMM.Test
{
    public class Issue3 : ITest
    {
        public Issue3()
            :base("Issue3",100)
        {
        }
        public void Check(TrainsitionPossibility p)
        {
            for(int i=0;i<p.StateSize;i++)
            {
                double r = 0;
                for (int j = 0; j < p.StateSize; j++)
                    r += p[i, j];
                Ensure.DoubleEqual(r, 1);
            }
        }
        public override void Run(UpdateTaskProgress update)
        {
            double[] p = new double[101];
            OutputSequence[] outputs = new OutputSequence[2];
            outputs[0] = new(0, 0, 1);
            outputs[1] = new(0, 1, 0, 1);
            HMM lamda = Learner.GetInitalHMM(3, 2);
            Learner learner=null;
            for (int i = 0; i < 100; i++)
            {
                learner = new(lamda, outputs);
                learner.Build();
                update(i+1);
                p[i] = learner.GetPossibility();
                lamda = learner.Compute();
                Check(lamda.A);
            }
            p[100] = learner.GetPossibility();
            UpdateInfo(p);
        }
    }
}
