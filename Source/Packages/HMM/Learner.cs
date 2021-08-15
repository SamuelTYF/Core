using System;

namespace HMM
{
    public class Learner
    {
        public int StateSize;
        public int OutputSize;
        public HMM Lambda;
        public int D;
        public OutputSequence[] Outputs;
        public CommonPossibility[] CommonPossibilities;
        public Learner(HMM lambda,OutputSequence[] outputs)
        {
            StateSize= lambda.StateSize;
            OutputSize=lambda.OutputSize;
            Lambda = lambda;
            D = outputs.Length;
            Outputs= outputs;
            CommonPossibilities = new CommonPossibility[D];
        }
        public void Build()
        {
            for (int d = 0; d < D; d++)
            {
                CommonPossibility cp = new(Lambda, Outputs[d]);
                cp.Build();
                CommonPossibilities[d] = cp;
            }
        }
        public StatePossibility ComputePi()
        {
            StatePossibility pi = new(StateSize);
            for(int state=0; state < StateSize; state++)
            {
                double p = 0;
                for (int d = 0; d < D; d++)
                    p += CommonPossibilities[d].Gamma[0][state];
                pi[state] = p / D;
            }
            return pi;                                                                                                                                                                                                                                                                                                                                                               
        }
        public TrainsitionPossibility ComputeA()
        {
            TrainsitionPossibility a = new(StateSize);
            for(int i=0;i<StateSize;i++)
                for(int j=0;j<StateSize;j++)
                {
                    double p = 0;
                    double q = 0;
                    for (int d = 0; d < D; d++) {
                        CommonPossibility cp = CommonPossibilities[d];
                        for (int t = 0; t < cp.T; t++)
                        {
                            p += cp.Ksi[t][i, j];
                            q += cp.Gamma[t][i];
                        }
                    }
                    a[i, j] = p / q;
                }
            return a;
        }
        public EmissionPossibility ComputeB()
        {
            EmissionPossibility b = new(StateSize, OutputSize);
            for (int state = 0; state < StateSize; state++)
            {
                double[] ps = new double[OutputSize];
                double q = 0;
                for (int d = 0; d < D; d++)
                {
                    CommonPossibility cp = CommonPossibilities[d];
                    OutputSequence output = Outputs[d];
                    for (int t = 0; t < cp.T; t++)
                    {
                        ps[output[t]] += cp.Gamma[t][state];
                        q += cp.Gamma[t][state];
                    }
                }
                for(int o=0;o<OutputSize;o++)
                b[state, o] = ps[o] / q;
            }
            return b;
        }
        public double GetPossibility()
        {
            double r = 0;
            for (int d = 0; d < D; d++)
                r += CommonPossibilities[d].Alpha.GetPossibility();
            return r;
        }
        public static readonly Random _R = new(DateTime.Now.Millisecond);
        public static double[] Random(int length)
        {
            double[] r = new double[length];
            double s = 0;
            for (int i = 0; i < length; i++)
                s += r[i] = _R.NextDouble();
            for (int i = 0; i < length; i++)
                r[i] /= s;
            return r;
        }
        public static HMM GetInitalHMM(int statesize,int outputsize)
        {
            HMM lambda = new(statesize, outputsize);
            for (int i = 0; i < statesize; i++)
            {
                double[] a = Random(statesize);
                for (int j = 0; j < statesize; j++)
                    lambda.A[i, j] = a[j];
                double[] b = Random(outputsize);
                for(int o=0;o<outputsize;o++)
                    lambda.B[i,o] = b[o];
            }
            lambda.Pi.Possibilities = Random(statesize);
            return lambda;
        }
        public HMM Compute()
            => new(StateSize, OutputSize)
            {
                A = ComputeA(),
                B = ComputeB(),
                Pi = ComputePi()
            };
    }
}
