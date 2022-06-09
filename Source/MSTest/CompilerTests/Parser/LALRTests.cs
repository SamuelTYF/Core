using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Parser.Tests
{
    [TestClass()]
    public class LALRTests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            LALR lalr = new();
            lalr.Register(Properties.Resources.Test_LALR);
            Assert.AreEqual(lalr.Errors.Count, 0);
            lalr.ComputeFirst();
            lalr.CreateClosures();
            Assert.IsTrue(lalr.Errors.Count > 0);
        }
    }
}