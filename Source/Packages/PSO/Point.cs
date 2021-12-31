using System;
using System.Collections.Generic;

namespace PSO
{
    public class Point
    {
        public List<Vector> PTraces;
        public Vector V;
        public Vector P;
        public double S;
        public Vector BestP;
        public double BestS=double.PositiveInfinity;
        public Point(Vector p)
        {
            V = new(p.Length);
            P = p;
            PTraces = new();
        }
        public void Update(Assess assess)
        {
            PTraces.Add(P);
            S = assess(P.Values);
            if(S<BestS)
            {
                BestP = P;
                BestS = S;
            }
        }
        public void Move(Random r, double rate, double grate, double prate, Vector gbest)
        {
            V = V * rate + grate * r.NextDouble() * (gbest - P) + prate * r.NextDouble() * (BestP - P);
            P += V;
        }
    }
}
