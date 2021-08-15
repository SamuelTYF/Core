namespace NumericalAnalysis
{
	public class PolynomialFraction
	{
		public Polynomial P;
		public Polynomial Q;
		public double this[double x] => P[x] / Q[x];
		public PolynomialFraction(Polynomial p, Polynomial q)
		{
			P = p;
			Q = q;
		}
		public PolynomialFraction Reduce()
		{
			if (P.IsZero())
				return new(new(default(double)), new(1.0));
			Polynomial t = ((P.P[P.Order] * Q.P[Q.Order] < 0.0) ? Polynomial.GCD(P, -Q) : Polynomial.GCD(P, Q));
			if (t.Order == 0)
				return this;
            Polynomial b = Q.Divide(t, out _);
			return new(P.Divide(t, out _) * (1.0 / b.P[b.Order]), b * (1.0 / b.P[b.Order]));
		}
        public static PolynomialFraction operator *(PolynomialFraction a, double b) => new(a.P * b, a.Q);
        public static PolynomialFraction operator *(PolynomialFraction a, PolynomialFraction b) => new(a.P * b.P, a.Q * b.Q);
        public static PolynomialFraction operator /(PolynomialFraction a, PolynomialFraction b) => new(a.P * b.Q, a.Q * b.P);
        public static PolynomialFraction operator +(PolynomialFraction a, PolynomialFraction b) => new(a.P * b.Q + a.Q * b.P, a.Q * b.Q);
        public static PolynomialFraction operator -(PolynomialFraction a, PolynomialFraction b) => new(a.P * b.Q - a.Q * b.P, a.Q * b.Q);
        public static PolynomialFraction operator -(double a, PolynomialFraction b) => new(a * b.Q - b.P, b.Q);
        public static PolynomialFraction operator -(PolynomialFraction a, double b) => new(a.P - b * a.Q, a.Q);
        public override string ToString() => $"({P})/({Q})";
    }
}
