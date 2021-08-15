using Collection;

namespace NumericalAnalysis.Cluster
{
    public class KMeans
    {
        public class Point
        {
            public double[] Values;
            public Point(double[] values) => Values = values;
            public static Point operator+(Point a, Point b)
            {
                if (a.Values.Length != b.Values.Length) throw new System.Exception();
                double[] r = new double[a.Values.Length];
                for (int i = 0; i < r.Length; i++)
                    r[i] = a.Values[i] + b.Values[i];
                return new(r);
            }
            public static Point operator /(Point a, double b)
            {
                double[] r = new double[a.Values.Length];
                for (int i = 0; i < r.Length; i++)
                    r[i] = a.Values[i]/b;
                return new(r);
            }
        }
        public class Kernel
        {
            public Point Point;
            public List<Point> Points;
            public Point Sum;
            public Kernel(Point point)
            {
                Point = point;
                Points = new();
            }
            public void Clear()
            {
                Points = new();
                Sum = new(new double[Point.Values.Length]);
            }
            public void Register(Point p)
            {
                Points.Add(p);
                Sum += p;
            }
            public void Update()
            {
                if (Points.Length == 0) return;
                Point = Sum / Points.Length;
            }
            public double GetDistance(Distance dis)
            {
                double r = 0;
                foreach (Point p in Points)
                    r += dis(p.Values, Point.Values);
                return r;
            }
        }
        public List<Kernel> Kernels;
        public List<Point> Points;
        public Distance GetDistance;
        public KMeans(Distance distance)
        {
            Kernels = new();
            Points = new();
            GetDistance = distance;
        }
        public void RegisterKernel(params Kernel[] kernels) => Kernels.AddRange(kernels);
        public void RegisterPoints(params Point[] points) => Points.AddRange(points);
        public double Cluster()
        {
            foreach (Kernel k in Kernels)
                k.Clear();
            foreach(Point p in Points)
            {
                double dis = double.PositiveInfinity;
                Kernel t = null;
                foreach (Kernel k in Kernels)
                {
                    double d = GetDistance(p.Values, k.Point.Values);
                    if(d<dis)
                    {
                        dis = d;
                        t = k;
                    }
                }
                t.Register(p);
            }
            foreach (Kernel k in Kernels)
                k.Update();
            double r = 0;
            foreach (Kernel k in Kernels)
                r += k.GetDistance(GetDistance);
            return r;
        }
    }
}
