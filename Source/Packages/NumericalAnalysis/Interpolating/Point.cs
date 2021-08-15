namespace NumericalAnalysis.Interpolating
{
	public struct Point
	{
		public double X;
		public double Y;
		public double D;
		public Point(double x, double y, double d = 0.0)
		{
			X = x;
			Y = y;
			D = d;
		}
        public static implicit operator Point((double x, double y) p) => new(p.x, p.y);
        public static implicit operator Point((double x, double y, double d) p) => new(p.x, p.y, p.d);
    }
}
