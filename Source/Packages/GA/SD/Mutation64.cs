using System;

namespace GA.SD
{
    public class Mutation64 : IMutation<Code64>
    {
        public double Rate;
        public Code64Factory Factory;
        public Mutation64(double rate,Code64Factory factory)
        {
            Rate = rate;
            Factory = factory;
        }
        public Code64 Mutation(Code64 source)
        {
            ulong value = source.Value;
            foreach(ulong mask in Factory.Masks)
                if(RandomHelper.NextDouble()<=Rate)
                {
                    ulong t = value ^ mask;
                    if (Factory.IsLegal(t)) value = t;
                }
            return new(value);
        }
    }
}
