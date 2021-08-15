using SAA._2D;
using SAA.OL;
using TestFramework;
namespace SAA.Test
{
    public class TSP:ITest
    {
        public Assess<PointOL> Assess;
        public int[,] Values;
        public int Length;
        public static int GCD(int x, int y) => y == 0 ? x : GCD(y, x % y);
        public const int Count = 10000;
        public TSP()
            : base("TSP", Count)
        {
            Length = 1000;
            Values = new int[Length, Length];
            for (int i = 0; i < Length; i++)
                for (int j = 0; j < Length; j++)
                    Values[i, j] = GCD(i + 1, j + 1);
            Assess = (PointOL point) =>
            {
                int[] xs = point.Indexs;
                double r = Values[xs[Length - 1], xs[0]];
                for (int i = 1; i < Length; i++)
                    r += Values[xs[i - 1], xs[i]];
                return r;
            };
        }
        public override void Run(UpdateTaskProgress update)
        {
            PointOL startpoint=PointOL.GetRandom(Length);
            double score = double.PositiveInfinity;
            Manager<PointOL> manager = new(
                new PointGeneratorOL(),
                new TemperatureControl(0.995, 10000, 1E-13));
            for (int i = 1; i <= Count; i++)
            {
                manager.GetResult(startpoint, Assess, out startpoint, out score);
                UpdateInfo(score);
                update(i);
            }
        }
    }
}
