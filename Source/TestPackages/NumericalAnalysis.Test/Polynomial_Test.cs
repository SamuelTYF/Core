using TestFramework;
namespace NumericalAnalysis.Test
{
    public class Polynomial_Test:ITest
    {
        public Polynomial_Test()
            :base("Polynomial_Test",10)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Polynomial r = new(-1, 1);
            double t = -2;
            for(int i=0;i<10;i++)
            {
                Ensure.DoubleEqual(r[-1], t);
                r *= new Polynomial(-1, 1);
                t *= -2;
                update(i + 1);
            }
        }
    }
}
