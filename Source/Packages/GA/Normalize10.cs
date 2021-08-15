namespace GA
{
    public class Normalize10 : INormalize
    {
        public double Max;
        public double Min;
        public double A;
        public double B;
        public void Inital(double min, double max)
        {
            Max = max;
            Min = min;
            A = max / ((min-max) * 2);
            B = max - A * min;
        }
        public double Normalize(double value) => A * value + B;
    }
}
