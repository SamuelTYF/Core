using NumericalAnalysis.Convolve;
using TestFramework;

namespace NumericalAnalysis.Test
{
    public class Convolve_Test :ITest
    {
        public Convolve_Test()
            :base("Convolve_Test",1000)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Vector a = new(1, 2, 3, 3, 2, 1);
            Vector b = new(1, 2, 3);
            b.Scale();
            Vector c = a.Convolve(b);
            BlindLR deconvolve = new(c, 3);
            for(int i=0;i<1000;i++)
            {
                deconvolve.F = deconvolve.GetNextF();
                deconvolve.Kernel = deconvolve.GetNextKernel();
                deconvolve.Kernel.Scale();
                update(i + 1);
                UpdateInfo(deconvolve.F.Convolve(deconvolve.Kernel,1));
            }
            UpdateInfo(deconvolve.F);
            UpdateInfo(deconvolve.Kernel);
        }
    }
}
