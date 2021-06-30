using System;
using NumericalAnalysis.Root;
using TestFramework;
namespace NumericalAnalysis.Test
{
    public class Root_Test:ITest
    {
        public Root_Test()
            :base("Root_Test",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Random _R = new(DateTime.Now.Millisecond);
            double[] values = new double[5];
            Polynomial f1;
            do
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = _R.Next(10);
                f1 = new(values);
                UpdateInfo(f1);
            } while (f1[-10] * f1[10] > 0);
            update(1);
            double x = Newton.FindRoot(f1, 10);
            UpdateInfo(x);
            Ensure.DoubleEqual(f1[x],0);
            update(2);
            x = Halley.FindRoot(f1, 10);
            UpdateInfo(x);
            Ensure.DoubleEqual(f1[x], 0);
            update(3);
            Complex cx = HalleyIrrational.FindRoot(f1, new Complex(10));
            UpdateInfo(cx);
            Ensure.DoubleEqual(f1[cx].Norm(), 0);
            update(4);
        }
    }
}
