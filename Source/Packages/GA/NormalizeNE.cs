using System;

namespace GA
{
    public class NormalizeNE : INormalize
    {
        public double Min;
        public double Max;
        public double A;
        public double B;
        public void Inital(double min, double max)
        {
            Min = min;
            Max = max;
            A = 1 / ((min - max) * 2);
            B = 1 - A * min;
        }
        public double Normalize(double value) => Math.Exp(A * value + B);
    }
}
