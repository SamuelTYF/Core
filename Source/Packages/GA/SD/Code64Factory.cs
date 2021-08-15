using System;
namespace GA.SD
{
    public class Code64Factory : ICodeFactory<Code64, double>
    {
        public double Min;
        public double Max;
        public ulong MinValue;
        public ulong MaxValue;
        public ulong Capacity;
        public ulong[] Masks;
        public Code64Factory(double min, double max)
        {
            Min = min;
            Max = max;
            MinValue = ToUInt64(min);
            MaxValue = ToUInt64(max);
            Capacity = MaxValue - MinValue;
            ulong[] ms = new ulong[64];
            ulong m = 1;
            int l = 0;
            while (l < 64 && m < Capacity)
            {
                ms[l] = m;
                m <<= 1;
                l++;
            }
            Masks = new ulong[l];
            for (int i = 0; i < l; i++)
                Masks[i] = ms[l - i - 1];
        }
        public static ulong ToUInt64(double value) => BitConverter.ToUInt64(BitConverter.GetBytes(value));
        public static double ToDouble(ulong value) => BitConverter.ToDouble(BitConverter.GetBytes(value));
        public double Decode(Code64 code) => ToDouble(code.Value)+Min;
        public Code64 Encode(double value) => new(ToUInt64(value-Min));
        public bool IsLegal(Code64 code) => code.Value < Capacity;
        public bool IsLegal(ulong value) => value < Capacity;
        public bool IsLegal(double value) => value > Min && value < Max;
        public void Random(out Code64 code, out double value)
        {
            do value = (Max - Min) * RandomHelper.NextDouble() + Min; while (!IsLegal(value));
            code = Encode(value);
        }
    }
}
