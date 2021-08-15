namespace HMM
{
    public class Backward
    {
        public int StateSize;
        public int OutputSize;
        public HMM Lambda;
        public int T;
        public OutputSequence Output;
        public StatePossibility[] StatePossibilities;
        public Backward(HMM lambda,OutputSequence output)
        {
            StateSize= lambda.StateSize;
            OutputSize= lambda.OutputSize;
            Lambda = lambda;
            T = output.T;
            Output= output;
            StatePossibilities = new StatePossibility[T];
        }
        public StatePossibility InitalBackwardPossibility()
        {
            StatePossibility ps=new(StateSize);
            for (int i = 0; i < StateSize; i++)
                ps[i] = 1;
            return ps;
        }
        public StatePossibility GetBackwardPossibility(StatePossibility last, int output)
        {
            StatePossibility ps=new(StateSize);
            for (int i = 0; i < StateSize; i++)
            {
                double p = 0;
                for (int j = 0; j < StateSize; j++)
                    p += Lambda.A[i, j] * last[j] * Lambda.B[j, output];
                ps[i] = p;
            }
            return ps;
        }
        public void Build()
        {
            StatePossibility p;
            StatePossibilities[T-1] = p=InitalBackwardPossibility();
            for (int t = T - 2; t >= 0; t--)
                StatePossibilities[t] = p = GetBackwardPossibility(p, Output[t + 1]);
        }
        public double GetPossibility()
        {
            double r = 0;
            int output = Output[0];
            StatePossibility p = StatePossibilities[0];
            for (int state = 0; state < StateSize; state++)
                r += Lambda.Pi[state]*p[state]*Lambda.B[state,output];
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
