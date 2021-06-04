namespace NumericalAnalysis
{
    public interface IFunction
    {
        int Dimension { get; }
        public double this[double[] xs] { get; }
        public IFunction GetDerivatives();
    }
}
