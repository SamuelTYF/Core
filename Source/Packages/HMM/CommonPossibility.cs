namespace HMM
{
    public class CommonPossibility
    {
        public int StateSize;
        public HMM Lambda;
        public int T;
        public OutputSequence Output;
        public Forward Alpha;
        public Backward Beta;
        public StatePossibility[] Gamma;
        public DoubleStatePossibility[] Ksi;
        public CommonPossibility(HMM lambda,OutputSequence output)
        {
            StateSize = lambda.StateSize;
            Lambda = lambda;
            T=output.T;
            Output = output;
            Alpha = new(lambda, output);
            Beta = new(lambda, output);
            Gamma = new StatePossibility[T];
            Ksi = new DoubleStatePossibility[T];
        }
        public void Build()
        {
            Alpha.Build();
            Beta.Build();
            for(int t=0;t<T;t++)
            {
                StatePossibility ps = new(StateSize);
                StatePossibility alpha = Alpha.StatePossibilities[t];
                StatePossibility beta = Beta.StatePossibilities[t];
                for (int state = 0; state < StateSize; state++)
                    ps[state] = alpha[state] * beta[state];
                ps.Normalized();
                Gamma[t] = ps;
            }
            for(int t=0;t<T-1;t++)
            {
                DoubleStatePossibility dsp = new(StateSize);
                StatePossibility alpha = Alpha.StatePossibilities[t];
                StatePossibility beta = Beta.StatePossibilities[t+1];
                int o = Output[t+1];
                for (int s1 = 0; s1 < StateSize; s1++)
                    for (int s2 = 0; s2 < StateSize; s2++)
                        dsp[s1, s2] = alpha[s1] * Lambda.A[s1, s2] * Lambda.B[s2, o] * beta[s2];
                dsp.Normalized();
                Ksi[t] = dsp;
            }
            {
                DoubleStatePossibility dsp = new(StateSize);
                StatePossibility alpha = Alpha.StatePossibilities[T-1];
                for (int s1 = 0; s1 < StateSize; s1++)
                    for (int s2 = 0; s2 < StateSize; s2++)
                        dsp[s1, s2] = alpha[s1] * Lambda.A[s1, s2];
                dsp.Normalized();
                Ksi[T-1] = dsp;
            }
        }
    }
}
