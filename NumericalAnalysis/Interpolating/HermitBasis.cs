namespace NumericalAnalysis.Interpolating
{
	public class HermitBasis
	{
		public double[] Xs;
		public LagrangeBasis Basis;
        public HermitBasis(params double[] xs) => Basis = new LagrangeBasis(Xs = xs);
        public void GetBasis(int i, out Polynomial a, out Polynomial b)
		{
			Polynomial L = Basis.L(i);
			Polynomial L2 = L * L;
			b = L2 * new Polynomial(- Xs[i], 1.0);
			double t = 0.0;
			for (int k = 0; k < i; k++)
				t += 2.0 / (Xs[k] - Xs[i]);
			for (int j = i + 1; j < Xs.Length; j++)
				t += 2.0 / (Xs[j] - Xs[i]);
			a = L2 * new Polynomial(1.0 - t * Xs[i], t);
		}
	}
}
