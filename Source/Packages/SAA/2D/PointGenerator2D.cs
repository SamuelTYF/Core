namespace SAA._2D
{
    public class PointGenerator2D : IPointGenerator<Point2D>
    {
        public double GetD(double T) => T * RandomHelper.NextRange();
        public Point2D Next(Point2D now, double T)=>new(now.X+GetD(T), now.Y+GetD(T));
    }
}
