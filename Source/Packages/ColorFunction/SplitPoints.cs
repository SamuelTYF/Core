using Collection;
using NumericalAnalysis;
using NumericalAnalysis.Interpolating;
namespace ColorFunction
{
    public class SplitPoints
    {
        public AVL<double, RGB> Points;
        public SplitPoints() => Points = new AVL<double, RGB>();
        public void Register(double x, RGB rgb) => Points[x] = rgb;
        public Point[] GetR()
        {
            List<Point> points = new();
            Points.LDR((x, r) => points.Add(new(x, r.R)));
            return points.ToArray();
        }
        public Point[] GetG()
        {
            List<Point> points = new();
            Points.LDR((x, r) => points.Add(new(x, r.G)));
            return points.ToArray();
        }
        public Point[] GetB()
        {
            List<Point> points = new();
            Points.LDR((x, r) => points.Add(new(x, r.B)));
            return points.ToArray();
        }
        public Polynomial GetRFunction() => new Newton(GetR()).Interpolate();
        public Polynomial GetGFunction() => new Newton(GetG()).Interpolate();
        public Polynomial GetBFunction() => new Newton(GetB()).Interpolate();
        public RGBFunction GetColorFunction() => new(GetRFunction(), GetGFunction(), GetBFunction());
    }
}
