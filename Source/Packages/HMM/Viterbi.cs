namespace HMM
{
    public class Viterbi
    {
        public int StateSize;
        public int OutputSize;
        public HMM Lambda;
        public int T;
        public OutputSequence Output;
        public int[,] PreState;
        /// <summary>
        /// Pti represents the maximum possibility of state i with o1-ot
        /// </summary>
        public StatePossibility[] StatePossibility;
        public Viterbi(HMM lambda,OutputSequence output)
        {
            StateSize = lambda.StateSize;
            OutputSize = lambda.OutputSize;
            Lambda = lambda;
            T = output.T;
            Output = output;
            PreState = new int[T, StateSize];
            StatePossibility = new StatePossibility[T];
        }
        private StatePossibility InitalStatePossibility(int output)
        {
            StatePossibility ps = new(StateSize);
            for (int state = 0; state < StateSize; state++)
                ps[state] = Lambda.Pi[state] * Lambda.B[state, output];
            return ps;
        }
        private StatePossibility GetStatePossibility(int t,StatePossibility last, int output)
        {
            StatePossibility ps = new(StateSize);
            for(int i=0;i<StateSize;i++)
            {
                double max = 0;
                for(int j=0;j<StateSize;j++)
                {
                    double p = last[j] * Lambda.A[j, i];
                    if(p>max)
                    {
                        max = p;
                        PreState[t, i] = j;
                    }
                }
                ps[i] = max * Lambda.B[i, output];
            }
            return ps;
        }
        public void Build()
        {
            StatePossibility ps;
            StatePossibility[0] = ps = InitalStatePossibility(Output[0]);
            for (int t = 1; t < T; t++)
                StatePossibility[t] = ps = GetStatePossibility(t, ps, Output[t]);
        }
        public int[] GetState()
        {
            int[] states = new int[T];
            StatePossibility ps = StatePossibility[^1];
            for (int state = 0; state < StateSize; state++)
                if (ps[state] > ps[states[^1]])
                    states[^1] = state;
            for (int t = T - 1; t > 0; t--)
                states[t - 1] = PreState[t, states[t]];
            return states;
        }
    }
}
