using System;
namespace TestFramework
{
    public class IsNullException:Exception
    {
        public IsNullException()
            :base()
        {
        }
        public override string ToString() => "IsNullException";
    }
}
