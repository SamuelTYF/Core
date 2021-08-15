namespace GA
{
    public interface INormalize
    {
        public void Inital(double min, double max);
        public double Normalize(double value);
    }
}
