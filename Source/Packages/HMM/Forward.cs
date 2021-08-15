namespace HMM
{
    public class Forward
    {
        public int StateSize;
        public HMM Lambda;
        public int T;
        public OutputSequence Output;
        /// <summary>
        /// t \in [0,T-1]
        /// </summary>
        public StatePossibility[] StatePossibilities;
        public Forward(HMM lambda, OutputSequence output)
        {
            StateSize = lambda.StateSize;
            Lambda = lambda;
            T = output.T;
            Output = output;
            StatePossibilities = new StatePossibility[T];
        }
        private StatePossibility InitalForwardPossibility(int output)
        {
            StatePossibility ps=new(StateSize);
            for(int state=0;state<StateSize;state++)
                ps[state]=Lambda.Pi[state]*Lambda.B[state,output];
            return ps;
        }
        private StatePossibility GetForwardPossibility(StatePossibility last,int output)
        {
            StatePossibility ps=new(StateSize);
            for(int i=0;i<StateSize;i++)
            {
                double p = 0;
                for (int j = 0; j < StateSize; j++)
                    p += last[j] * Lambda.A[j, i];
                ps[i] = p * Lambda.B[i, output];
            }
            return ps;
        }
        public void Build()
        {
            StatePossibility p = InitalForwardPossibility(Output[0]);
            StatePossibilities[0] = p;
            for (int t = 1; t < T; t++)
                StatePossibilities[t] = p = GetForwardPossibility(p, Output[t]);
        }
        public double GetPossibility()
        {
            double r = 0;
            StatePossibility ps = StatePossibilities[^1];
            for (int state = 0; state < StateSize; state++)
                r += ps[state];
            return r;
        }
        public static double GetPossibility(HMM lambda, OutputSequence output)
        {
            Forward forward = new(lambda, output);
            forward.Build();
            return forward.GetPossibility();
        }
    }
}
