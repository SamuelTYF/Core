using System;

namespace SAA
{
    public static class RandomHelper
    {
        public static readonly Random _R = new(DateTime.Now.Millisecond);
        public static double NextDouble()=>_R.NextDouble();
        public static int Next(int max)=>_R.Next(max);
        public static double NextRange() => _R.NextDouble() * 2 - 1;
    }
}
