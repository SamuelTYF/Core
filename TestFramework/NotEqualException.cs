using System;
namespace TestFramework
{
    public class NotEqualException:Exception
    {
        public object A;
        public object B;
        public NotEqualException(object a, object b):base()
        {
            A = a;
            B = b;
        }
    }
}
