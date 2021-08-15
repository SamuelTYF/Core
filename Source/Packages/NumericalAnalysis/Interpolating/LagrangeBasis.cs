using System;
using Collection;
namespace NumericalAnalysis.Interpolating
{
	public class LagrangeBasis
	{
		public double[] Xs;
		public BlockTree<Frequency> Total;
        public LagrangeBasis(params double[] xs) => Xs = xs;
        public void ComputePi() => Total = new BlockTree<Frequency>((a,b) => a * b, Array.ConvertAll(Xs, (double x) => new Polynomial(-x, 1.0).FFT_2_Time_Position(Xs.Length + 1)));
        public Polynomial L(int i)
		{
			Polynomial r = new(1.0);
			double xi = Xs[i];
			double[] xs = Xs;
			foreach (double xk in xs)
				if (xk != xi)
					r *= new Polynomial(xk / (xk - xi), 1.0 / (xi - xk));
			return r;
		}
        public Frequency F(int i) => Total.GetExcept(i);
    }
}
