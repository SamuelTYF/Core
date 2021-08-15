using System;

namespace GA.OL
{
    public class CrossoverOL : ICrossover<CodeOL>
    {
        public double Rate;
        public int Length;
        public CrossoverOL(double rate, int length)
        {
            Rate = rate;
            Length = length;
        }
        public CodeOL Generate(int[] a, int[] b, int l,int h)
        {
            int[] values = new int[Length];
            bool[] existed= new bool[Length];
            for (int i = l; i <= h; i++)
                existed[values[i] = b[i]] = true;
            int p = 0;
            for(int i=0;i<Length;i++)
            {
                if (p == l) p = h+1;
                if (!existed[a[i]]) values[p++] = a[i];
            }
            return new(values);
        }
        public void Crossover(CodeOL father, CodeOL mother, out CodeOL son, out CodeOL daughter)
        {
            if(RandomHelper.NextDouble()>=Rate)
            {
                son = father;
                daughter = mother;
                return;
            }
            int[] a = father.Values;
            int[] b = mother.Values;
            int h = RandomHelper.Next(Length);
            int l= RandomHelper.Next(Length);
            if (h < l)
            {
                int t = h;
                h = l;
                l = t;
            }
            son=Generate(a,b,l,h);
            daughter = Generate(b, a, l, h);
        }
    }
}
