using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries.Tests
{
    [TestClass]
    public class WaveletTests
    {
        [TestMethod]
        public void DaubechiesTest()
        {
            var result=Wavelet.Daubechies(2,1e-10);
            var d4 = Wavelet.D4;
            Assert.AreEqual(result.Length, 4);
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(result[i], d4[i], 1e-10);
        }
    }
}