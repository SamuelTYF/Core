using NumericalAnalysis.Minimum;
using TestFramework;
namespace NumericalAnalysis.Test
{
    public class IMinimum_Test:ITest
    {
        public IMinimum_Test()
            :base("IMinimum_Test", 4)
        {
        }
        public double F1(double[] xs)
        {
            double x = xs[0];
            double y = xs[1];
            return x * x + 7 * x + y * y;
        }
        public double F2(double[] xs)
        {
            double x = xs[0];
            double y = xs[1];
            double z = xs[2];
            return x * x + y * y + z * z;
        }
        public override void Run(UpdateTaskProgress update)
        {
            Simplex simplex = new(F1, new double[] { 5, 7 }, new double[] { 0.5, 1 }, 0.995, 0.00001);
            double r = simplex.Run(out double e);
            UpdateInfo(e);
            Ensure.DoubleRateLess(r, -12.25,0.001);
            update(1);
            Gradient newton = new(F1, new double[] { 5, 7 }, new double[] { 0.5, 1 }, 0.995, 0.00001);
            r = simplex.Run(out e);
            UpdateInfo(e);
            Ensure.DoubleRateLess(r, -12.25, 0.001);
            update(2);
            simplex = new(F2, new double[] { 5,-5,10}, new double[] { 0.5, 0.5,1 }, 0.995, 0.00001);
            r = simplex.Run(out e);
            UpdateInfo(e);
            Ensure.DoubleEqual(simplex.Xs[0], 0, 0.001);
            Ensure.DoubleEqual(simplex.Xs[1], 0, 0.001);
            Ensure.DoubleEqual(simplex.Xs[2], 0, 0.001);
            update(3);
            newton = new(F2, new double[] { 5, -5, 10 }, new double[] { 0.5, 0.5, 1 }, 0.995, 0.00001);
            r = newton.Run(out e);
            UpdateInfo(e);
            Ensure.DoubleEqual(newton.Xs[0], 0, 0.001);
            Ensure.DoubleEqual(newton.Xs[1], 0, 0.001);
            Ensure.DoubleEqual(newton.Xs[2], 0, 0.001);
            update(4);
        }
    }
}
