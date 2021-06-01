using System;
using System.IO;

namespace TestFramework
{
    public static class Ensure
    {
        public static void Equal<T>(T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) != 0) throw new NotEqualException(a, b);
        }
        public static void Equal<T>(T[] a, T[] b) where T : IComparable<T>
        {
            if (a.Length != b.Length) throw new NotEqualException(a, b);
            for (int i=0;i<a.Length;i++)
                if (a[i].CompareTo(b[i]) != 0)
                    throw new NotEqualException(a, b);
        }
        public static void Equal(Stream a,Stream b)
        {
            if(a.Length!=b.Length) throw new NotEqualException(a, b);
            byte[] sa = new byte[1 << 16];
            byte[] sb = new byte[1 << 16];
            while (a.Position<a.Length)
            {
                a.Read(sa, 0, sa.Length);
                b.Read(sb, 0, sa.Length);
                Equal(sa, sb);
            }
        }
    }
}
