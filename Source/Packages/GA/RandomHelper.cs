using System;

namespace GA
{
    public static class RandomHelper
    {
        public static readonly Random _R = new(DateTime.Now.Millisecond);
        public static double NextDouble()
        {
            lock(_R)
                return _R.NextDouble();
        }
        public static int Next(int max)
        {
            lock (_R)
                return _R.Next(max);
        }
        public static ulong Next(ulong max)
        {
            byte[] bs = new byte[8];
            _R.NextBytes(bs);
            ulong value = BitConverter.ToUInt64(bs);
            return value % max;
        }
    }
}
