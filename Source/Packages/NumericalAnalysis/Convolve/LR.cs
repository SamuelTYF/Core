using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis.Convolve
{
    public class LR
    {
        public Vector Kernel;
        public Vector C;
        public Vector F;
        public int Offset;
        public LR(Vector kernel,Vector c,int offset)
        {
            Kernel = kernel;
            C = c;
            double[] t = new double[c.Length];
            Array.Fill(t, 1);
            F = new(t);
            Offset = offset;
        }
        public Vector GetNextF() => F * (C / F.Convolve(Kernel,Offset)).Convolve(~Kernel, Offset);
    }
    public class BlindLR
    {
        public Vector Kernel;
        public Vector C;
        public Vector F;
        public BlindLR(Vector c,int kernellength)
        {
            C = c;
            double[] k = new double[kernellength];
            Array.Fill(k, 1);
            Kernel = new(k);
            double[] t = new double[c.Length];
            Array.Fill(t, 1);
            F = new(t);
        }
        public Vector GetNextF() => F * (C / F.Convolve(Kernel)).Convolve(~Kernel);
        public Vector GetNextKernel() => Kernel * (C / F.Convolve(Kernel)).Convolve(~F).SubVector((Kernel.Length-1)/2, Kernel.Length);
    }
}
