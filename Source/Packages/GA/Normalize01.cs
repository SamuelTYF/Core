namespace GA
{
    public class Normalize01 : INormalize
    {
        public double A;
        public double B;
        public void Inital(double min, double max)
        {
            A = max / ((max - min) * 2);
            B = (1 - A) * max;
        }
        public double Normalize(double value) => A * value + B;
    }
}
