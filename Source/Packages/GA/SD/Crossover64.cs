using System;

namespace GA.SD
{
    public class Crossover64 : ICrossover<Code64>
    {
        public Code64Factory Factory;
        public Crossover64(Code64Factory factory) => Factory = factory;
        public void Crossover(Code64 father, Code64 mother, out Code64 son, out Code64 daughter)
        {
            ulong a = father.Value;
            ulong b = mother.Value;
            int h = RandomHelper.Next(Factory.Masks.Length);
            int l;
            do l = RandomHelper.Next(Factory.Masks.Length); while (l == h);
            if (h < l)
            {
                h ^= l;
                l ^= h;
                h ^= l;
            }
            ulong mask = (((ulong)1 << (h - l)) - 1)<<l;
            ulong am = a & mask;
            ulong bm = b & mask;
            ulong c = (a ^ am) | bm;
            ulong d = (b ^ bm) | am;
            son = new(Factory.IsLegal(c)?c:a);
            daughter = new(Factory.IsLegal(d) ? d : b);
        }
    }
}
