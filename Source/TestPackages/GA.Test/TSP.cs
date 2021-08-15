using TestFramework;
using GA.OL;
using System;

namespace GA.Test
{
    public class TSP:ITest
    {
        public Assess<int[]> Assess;
        public NormalizeNE Normalize;
        public int[,] Values;
        public int Length;
        public static int GCD(int x, int y) => y == 0 ? x : GCD(y, x % y);
        public const int Count = 10000;
        public TSP()
            :base("TSP",Count)
        {
            Length = 1000;
            Values = new int[Length, Length];
            for (int i = 0; i < Length; i++)
                for (int j = 0; j < Length; j++)
                    Values[i, j] = GCD(i + 1, j + 1);
            Assess = (int[] xs) =>
            {
                double r = Values[xs[Length-1],xs[0]];
                for (int i = 1; i < Length; i++)
                    r += Values[xs[i - 1], xs[i]];
                return r;
            };
            Normalize = new();
        }
        public override void Run(UpdateTaskProgress update)
        {
            CodeOLFactory factory = new(Length);
            EntityOLCollection collection = new(0, 1024, Assess, Normalize);
            collection.Inital(factory);
            EvolutionOL evolution = new(512,factory, new CrossoverOL(0.3,Length), new MutationOL(0.2, Length));
            double r;
            DateTime dt = DateTime.Now;
            for (int i=0;i<Count;i++)
            {
                EntityOLCollection next = evolution.Evolve(collection);
                r= collection[0].Score;
                collection = next;
                update(i);
                UpdateInfo($"{r},{DateTime.Now-dt}");
            }
            collection.Update();
            r=collection[0].Score;
            UpdateInfo($"{r},{DateTime.Now - dt}");
        }
    }
}
