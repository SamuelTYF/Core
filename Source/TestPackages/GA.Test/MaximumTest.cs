using GA.SD;
using System;
using TestFramework;

namespace GA.Test
{
    public class MaximumTest
    {
        public class MinimumTest : ITest
        {
            public Assess<double> Assess;
            public NormalizeN Normalize;
            public MinimumTest()
                : base("MaximumTest", 100)
            {
                Assess = (x) => -Math.Pow(x, 1/x);
                Normalize = new();
            }
            public override void Run(UpdateTaskProgress update)
            {
                Code64Factory factory = new(0, 5);
                Entity64Collection[] collections = new Entity64Collection[101];
                Entity64Collection collection = new(0, 1024, Assess, Normalize);
                collection.Inital(factory);
                collections[0] = collection;
                Evolution64 evolution = new(128, factory, new Crossover64(factory), new Mutation64(0.1, factory));
                for (int i = 1; i <= 100; i++)
                {
                    collections[i] = evolution.Evolve(collections[i - 1]);
                    update(i);
                }
                collections[^1].Update();
                double[] r = new double[101];
                for (int i = 0; i < 101; i++)
                    r[i] = collections[i][0].Score;
            }
        }
    }
}
