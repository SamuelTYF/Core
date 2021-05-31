using System;

namespace TestFramework.Test
{
    public class ITest_Test : ITest
    {
        public ITest_Test()
            : base("ITest_Test")
        {
        }
        public override void Run()
        {
            try
            {
                Ensure.Equal(1, 2);
                throw new Exception("Not Equal Exception Was Not Throwed");
            }
            catch (NotEqualException) { }
            try
            {
                Ensure.Equal(1, 1);
            }
            catch (NotEqualException)
            {
                throw new Exception("Not Equal Exception Was Throwed");
            }
        }
    }
}
