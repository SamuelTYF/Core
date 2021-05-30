using System;

namespace TestFramework
{
    public abstract class ITest
    {
        public string TestName;
        public ITest(string name) => TestName = name;
        public abstract void Run();
    }
}
