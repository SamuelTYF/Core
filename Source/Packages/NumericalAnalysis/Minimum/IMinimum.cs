using System;
namespace NumericalAnalysis.Minimum
{
	public abstract class IMinimum
	{
		public int Length;
		public Func<double[], double> F;
		public double[] Xs;
		public double[] Hs;
		public double Omega;
		public double Error;
		public IMinimum(Func<double[], double> f, double[] xs,double[] hs, double omega, double error)
		{
			F = f;
			Length = xs.Length;
			Xs = xs;
			Hs = hs;
			Omega = omega;
			Error = error;
		}
		public abstract double Run(out double e);
		public virtual string GetName() => "IMinimum";
    }
}
