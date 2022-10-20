using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeries;
namespace TimeSeriesTests
{
    [TestClass]
    public class BigIntegerTest
    {
        [TestMethod]
        public void Test()
        {
            Series a = new(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            Series b = new(1, 1, 1, 1, 1);
            int l = a.Length + b.Length;
            Frequency fa = a.FFT_2_Time_Position(l);
            Frequency fb = b.FFT_2_Time_Position(l);
            Frequency fr = fa * fb;
            Series c = fr.IFFT_2_Time_Position().Sub(l);
            Console.WriteLine(c);
            int t = 0;
            for(int i=0;i<l;i++)
            {
                int r = (int)(c.Values[i] + 0.5 + t);
                t = r / 10;
                c.Values[i] = r % 10;
            }
            Console.WriteLine(c);
        }
    }
}
