using TestFramework;
namespace HMM.Test
{
    public class Issue1:ITest
    {
        public Issue1()
            :base("Issue1",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            HMM lamda = new(3,2);
            lamda.A.Possibilities = new double[3, 3] {
                {0.6,0.3,0.1 },
                {0.3,0.4,0.3 },
                {0.1,0.3,0.6 }
            };
            lamda.B.Possibilities = new double[3, 2] {
                {0.7,0.3 },
                {0.6,0.4 },
                {0.2,0.8 }
            };
            lamda.Pi.Possibilities = new double[3] { 0.2, 0.5, 0.3 };
            OutputSequence output = new (0, 0, 1);
            update(1);
            double pf = Forward.GetPossibility(lamda,output);
            update(2);
            UpdateInfo(pf);
            double pb = Backward.GetPossibility(lamda, output);
            update(3);
            UpdateInfo(pb);
            Ensure.DoubleEqual(pf, pb);
            update(4);
            ForwardPredict fp = new(lamda, 3);
            fp.Build();
            string s = $"{string.Join(",", fp.GetOutput())}\n";
            for (int i=0;i<2;i++)
                for(int j=0;j<2;j++)
                    for(int t=0;t<2;t++)
                    {
                        output = new(i, j, t);
                        pf = Forward.GetPossibility(lamda,output);
                        pb = Backward.GetPossibility(lamda, output);
                        s += $"{string.Join(",", output)}\t{pf}\t{pb}\n";
                    }
            UpdateInfo(s);
        }
    }
}
