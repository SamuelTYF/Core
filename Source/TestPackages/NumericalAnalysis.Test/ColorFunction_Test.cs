using ColorFunction;
using System.Drawing;
using TestFramework;
namespace NumericalAnalysis.Test
{
    public class ColorFunction_Test:ITest
    {
        public ColorFunction_Test()
            :base("ColorFunction_Test",3)
        {
        }
        public static void Compare(RGB a,RGB b)
        {
            Ensure.DoubleEqual(a.R, b.R);
            Ensure.DoubleEqual(a.G, b.G);
            Ensure.DoubleEqual(a.B, b.B);
        }
        public override void Run(UpdateTaskProgress update)
        {
            RGB rgb = new(1, 1, 1);
            Color c = rgb;
            Ensure.Equal(c.R, 255);
            Ensure.Equal(c.G, 255);
            Ensure.Equal(c.B, 255);
            update(1);
            SplitPoints sp = new();
            sp.Register(0, new(1, 0, 0));
            sp.Register(0.5, new(0, 1, 0));
            sp.Register(1, new(0, 0, 1));
            RGBFunction function = sp.GetColorFunction();
            sp.Points.LDR((x, r) => Compare(function[x], r));
            UpdateInfo(function);
            update(2);
            function.GetBitmap(512,128).Save("1.jpg");
            update(3);
        }
    }
}
