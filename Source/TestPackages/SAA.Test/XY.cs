using TestFramework;
using SAA._2D;
namespace SAA.Test
{
    public class XY:ITest
    {
        public const int Count = 10000;
        public Assess<Point2D> Assess;
        public XY()
            :base("XY", Count)
        {
            Assess = (Point2D point) => point.X*point.X + point.Y*point.Y;
        }
        public override void Run(UpdateTaskProgress update)
        {
            Point2D startpoint = new(10, 10);
            double score = double.PositiveInfinity;
            Manager<Point2D> manager = new(
                new PointGenerator2D(),
                new TemperatureControl(0.95, 10000, 1E-13));
            for(int i=1;i<= Count; i++)
            {
                manager.GetResult(startpoint, Assess, out startpoint, out score);
                UpdateInfo($"({startpoint.X},{startpoint.Y}),{score}");
                update(i);
            }
        }
    }
}
