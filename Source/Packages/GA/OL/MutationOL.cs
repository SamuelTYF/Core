using System;

namespace GA.OL
{
    public class MutationOL : IMutation<CodeOL>
    {
        public double Rate;
        public int Length;
        public MutationOL(double rate,int length)
        {
            Rate = rate;
            Length = length;
        }
        public CodeOL Mutation(CodeOL source)
        {
            if (RandomHelper.NextDouble() >= Rate) return source;
            int[] values = new int[Length];
            Array.Copy(source.Values, 0, values, 0, Length);
            int h = RandomHelper.Next(Length);
            int l = RandomHelper.Next(Length);
            if (h < l)
            {
                int t = h;
                h = l;
                l = t;
            }
            int v = values[l];
            values[l] = values[h];
            values[h] = v;
            return new CodeOL(values);
        }
    }
}
