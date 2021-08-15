using TestFramework;
namespace HMM.Test
{
    public class Issue2:ITest
    {
        public Issue2()
            :base("Issue2",1)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            HMM lamda = new(3, 2);
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
            OutputSequence output = new(0, 0, 1);
            Viterbi viterbi = new(lamda, output);
            viterbi.Build();
            int[] states = viterbi.GetState();
            Ensure.Equal(states[0], 1);
            Ensure.Equal(states[1], 1);
            Ensure.Equal(states[2], 2);
            update(1);
        }
    }
}
