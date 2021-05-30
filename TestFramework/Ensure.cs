using System;
namespace TestFramework
{
    public static class Ensure
    {
        public static void Equal<T>(T a,T b) where T:IComparable<T>
        {
            if (a.CompareTo(b) != 0) throw new NotEqualException(a, b);
        }
    }
}
