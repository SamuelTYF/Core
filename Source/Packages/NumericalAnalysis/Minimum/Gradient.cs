using System;
namespace NumericalAnalysis.Minimum
{
	public class Gradient : IMinimum
	{
		public double Value;
		public double Min;
		public double Step = 1.0;
		public Gradient(Func<double[], double> f, double[] xs, double[] hs, double omega, double error)
			: base(f, xs, hs, omega, error) => Value = F(xs);
        public override double Run(out double e)
		{
			e = double.PositiveInfinity;
			do
			{
				Min = Value;
				double[] ds = new double[Length];
				for(int i=0;i<Length;i++)
                {
					double[] fxs = new double[Length];
					double[] bxs = new double[Length];
					Array.Copy(Xs, 0, fxs, 0, Length);
					Array.Copy(Xs, 0, bxs, 0, Length);
					fxs[i] += Step * Hs[i];
					bxs[i] -= Step * Hs[i];
					ds[i] = (F(fxs) - F(bxs)) / (2 * Step * Hs[i]);
				}
				for (int i = 0; i < Length; i++)
					Xs[i] -= Step * Hs[i] * ds[i];
				Step *= Omega;
				Value = F(Xs);
				e = Math.Abs((Value - Min) / Value);
			}
			while (e >= Error);
			return Value;
		}
		public override string GetName() => "Gradient";
	}
}
