namespace HMM
{
    public class ForwardPredict
    {
        public int StateSize;
        public int OutputSize;
        public HMM Lambda;
        public int T;
        public StatePossibility[] StatePossibilities;
        public ForwardPredict(HMM lambda,int t)
        {
            StateSize = lambda.StateSize;
            OutputSize = lambda.OutputSize;
            Lambda = lambda;
            T = t;
            StatePossibilities = new StatePossibility[T];
        }
        private StatePossibility GetStatePossibility(StatePossibility last)
        {
            StatePossibility ps = new(StateSize);
            for(int i=0; i < StateSize; i++)
            {
                double p = 0;
                for (int j = 0; j < StateSize; j++)
                    p += last[j] * Lambda.A[j, i];
                ps[i] = p;
            }
            return ps;
        }
        private int GetOutput(StatePossibility ps)
        {
            int output = 0;
            double max = 0;
            for(int o=0;o<OutputSize;o++)
            {
                double p = 0;
                for (int state = 0; state < StateSize; state++)
                    p += ps[state] * Lambda.B[state, o];
                if(p>max)
                {
                    max = p;
                    output = o;
                }
            }
            return output;
        }
        public void Build()
        {
            StatePossibility ps;
            StatePossibilities[0] = ps = Lambda.Pi;
            for (int t = 1; t < T; t++)
                StatePossibilities[t] = ps = GetStatePossibility(ps);
        }
        public int[] GetOutput()
        {
            int[] output = new int[T];
            for (int t = 0; t < T; t++)
                output[t] = GetOutput(StatePossibilities[t]);
            return output;
        }
    }
}
