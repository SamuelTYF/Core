using Collection;
using NumericalAnalysis.Cluster;
using System;
using TestFramework;
namespace NumericalAnalysis.Test
{
    public class KMeans_Test:ITest
    {
        public static readonly Random _R = new(DateTime.Now.Millisecond);
        public KMeans_Test()
            :base("KMeans_Test",9)
        {
        }
        public double Norm2(double[] xs,double[] ys)
        {
            double r = 0;
            Ensure.Equal(xs.Length, ys.Length);
            for (int i = 0; i < xs.Length; i++)
                r += Math.Sqrt((xs[i] - ys[i]) * (xs[i] - ys[i]));
            return r;
        }
        public KMeans.Point[] Create(int count,double[] p,double r,ref double sum)
        {
            Ensure.Equal(p.Length, 2);
            KMeans.Point[] ps = new KMeans.Point[count];
            for (int i=0;i<count;i++)
            {
                double d = _R.NextDouble() * r;
                double phi = _R.NextDouble() * Math.PI * 2;
                double[] t = { p[0] + d * Math.Cos(phi), p[1] + d * Math.Sin(phi) };
                sum += Norm2(t, p);
                ps[i]=new(t);
            }
            return ps;
        }
        public override void Run(UpdateTaskProgress update)
        {
            KMeans kmeans = new(Norm2);
            kmeans.RegisterKernel(
                new(new(new double[] { -5, 5 })),
                new(new(new double[] { 5, 5 })),
                new(new(new double[] { 5, -5 })));
            double r = 0;
            kmeans.RegisterPoints(Create(100, new double[] { 0, 0.5 }, 0.5, ref r));
            kmeans.RegisterPoints(Create(100, new double[] { 0, 2 }, 1, ref r));
            kmeans.RegisterPoints(Create(100, new double[] { 1, 1 }, 1, ref r));
            update(1);
            string s = "";
            for(int i=2;i<10;i++)
            {
                double t=kmeans.Cluster();
                update(i);
                s += $"{t}/{r}\n";
                UpdateInfo(s);
            }
        }
    }
}
