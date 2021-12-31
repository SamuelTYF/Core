using PSO;
using System;
using TestFramework;

namespace NumericalAnalysis.Test
{
    public class PSO_Test:ITest
    {
        public Assess Assess;
        public PSO_Test()
            : base("PSO_Test", 100) => Assess = (r) =>
            {
                double x = r[0];
                double y= r[1];
                return 2 * x * x + y * y + 3 * (x - 3) * (x - 3) + 4 * (y - 4) * (y - 4);
            };
        public override void Run(UpdateTaskProgress update)
        {
            Manager manager = new(Assess, 0.1, 2, 2);
            Random r = new(DateTime.Now.Millisecond);
            for (int i = 0; i < 100; i++)
                manager.RegisterPoint(r.NextDouble(), r.NextDouble());
            for(int i=0;i<100;i++)
            {
                manager.Update();
                update(i + 1);
                UpdateInfo($"{manager.BestS}\t{manager.BestP}");
                //manager.Print(-4, 4, -4, 4, 500, 500).Save($"{i}.jpg");
            }
        }
    }
}
