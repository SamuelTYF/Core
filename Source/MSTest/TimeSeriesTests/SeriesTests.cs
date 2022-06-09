using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries.Tests
{
    [TestClass()]
    public class SeriesTests
    {
        [TestMethod()]
        public void FFT_2_Time_IterationTest()
        {
            Series series = new(1,2,3,4);
            Series frequency =series.FFT_2_Time_Iteration().Abs();
            Assert.AreEqual(frequency.Values[0], 10);
            Assert.AreEqual(frequency.Values[1], 2*Math.Sqrt(2),1e-10);
            Assert.AreEqual(frequency.Values[2], 2);
            Assert.AreEqual(frequency.Values[3], 2 * Math.Sqrt(2), 1e-10);
        }

        [TestMethod()]
        public void FFT_2_Time_PositionTest()
        {
            Series series = new(1, 2, 3, 4);
            Series result = series.FFT_2_Time_Position().IFFT_2_Time_Position();
            Assert.AreEqual(series.Length, result.Length);
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(series.Values[i], result.Values[i],1e-10);
        }
    }
}