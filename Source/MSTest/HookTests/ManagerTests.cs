using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Tests
{
    [TestClass()]
    public class ManagerTests
    {
        [TestMethod()]
        public void StartTest()
        {
            Manager manager = new("ResourceForm.exe");
            manager.Start();
            long last = DateTime.Now.AddMinutes(1).Ticks;
            long tick = (long)manager.Execute("GetTicks");
            manager.Dispose();
            Assert.IsTrue(tick < last);
        }
    }
}