using System;
using System.Collections;
using System.Linq;

namespace RBM
{
    public class RBM
    {
        public int M;
        public int N;
        public double[] A;
        public double[] B;
        public double[,] W;
        public Random _R;
        public double[][] Vs;
        public double[][] Hs;
        public RBM(int m,int n)
        {
            M = m;
            N = n;
            _R = new(DateTime.Now.Millisecond);
            A = new double[m];
            B = new double[n];
            W = new double[m, n];
            Vs = Get(M - 1, M).ToArray();
            Hs = Get(N - 1, N).ToArray();
        }
        public static System.Collections.Generic.IEnumerable<double[]> Get(int l,int len)
        {
            if (l < 0) yield return new double[len];
            else
            {
                foreach (double[] p in Get(l - 1, len))
                    yield return p;
                foreach (double[] p in Get(l - 1, len))
                {
                    p[l] = 1;
                    yield return p;
                }
            }
        }
        public void RandomInital()
        {
            for (int i = 0; i < M; i++) A[i] = _R.NextDouble();
            for(int j=0; j < N; j++) B[j] = _R.NextDouble();
            for(int i = 0; i < M;i++)
                for(int j = 0; j < N;j++)
                    W[i,j] = _R.NextDouble();
        }
        public double GetE(double[] v,double[] h)
        {
            double e = 0;
            for (int i = 0; i < M; i++)
                    e += A[i]* v[i];
            for (int j = 0; j < N; j++)
                    e += B[j]* h[j];
            for (int i = 0; i < M; i++)
                    for (int j = 0; j < N; j++)
                            e += v[i]*W[i, j]* h[j];
            return -e;
        }
        public double Sigmoid(double e) => 1 / (1 + Math.Exp(-e));
        public double GetPHj(int j,double[] v)
        {
            double e = B[j];
            for (int i = 0; i < M; i++)
                    e += W[i, j]* v[i];
            return Sigmoid(e);
        }
        public double GetPVi(int i, double[] h)
        {
            double e = A[i];
            for (int j = 0; j < N; j++)
                    e += W[i, j]* h[j];
            return Sigmoid(e);
        }
        public double[] GetH(double[] v)
        {
            double[] h=new double[N];
            for (int j = 0; j < N; j++)
                h[j] = GetPHj(j,v);
            return h;  
        }
        public double[] GetV(double[] h)
        {
            double[] v = new double[M];
            for (int i = 0; i < M; i++)
                v[i] = GetPVi(i, h);
            return v;
        }
        public double[] SampleV(double[] h)
        {
            double[] p = GetV(h);
            for (int i = 0; i < M; i++)
                p[i] = _R.NextDouble() <= p[i]?1:0;
            return p;
        }
        public void Learn(double[] v0, double[] da, double[] db, double[,] dw)
        {
            double[] h0 = GetH(v0);
            double[] v1 = GetV(h0);
            for (int i = 0; i < M; i++)
                da[i] += v0[i] - v1[i];
            for (int j = 0; j < N; j++)
                db[j] += GetPHj(j, v0) - GetPHj(j, v1);
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    dw[i, j] += GetPHj(j, v0) * v0[i] - GetPHj(j, v1) * v1[i];
        }
        public void Learn(double[][] s,int T,double rate)
        {
            rate /= s.Length;
            for(int t=0;t<T;t++)
            {
                double[] da = new double[M];
                double[] db = new double[N];
                double[,] dw = new double[M,N];
                foreach (double[] v in s)
                    Learn(v, da, db, dw);
                for (int i = 0; i < M; i++)
                    A[i] += rate * da[i];
                for (int j = 0; j < N; j++)
                    B[j] += rate * db[j];
                for (int i = 0; i < M; i++)
                    for (int j = 0; j < N; j++)
                        W[i, j] += rate * dw[i, j];
            }
        }
        public double GetP(double[] v, double[] h) => Math.Exp(-GetE(v, h));
        public double GetLogP(double[][] s)
        {
            double r = 0;
            double z = 0;
            foreach (double[] h in Hs)
                foreach (double[] v in Vs)
                    z += GetP(v, h);
            foreach (double[] v in s)
            {
                double p = 0;
                foreach (double[] h in Hs)
                    p += GetP(v, h);
                r += Math.Log(p / z);
            }
            return r;
        }
    }
}