using System;
using System.Threading;

namespace GA.OL
{
    public class EvolutionOL : IEvolution<CodeOL, int[], EntityOLCollection>
    {
        public int RestoreCount;
        public CodeOLFactory Factory;
        public CrossoverOL Crossover;
        public MutationOL Mutation;
        public int ThreadCount=8;
        public EvolutionOL(int restorecount,CodeOLFactory factory, CrossoverOL crossover,MutationOL mutation)
        {
            RestoreCount = restorecount;
            Factory = factory;
            Crossover= crossover;
            Mutation= mutation;
        }
        public EntityOLCollection Evolve(EntityOLCollection current)
        {
            current.Update();
            EntityOLCollection next = new(current.Generation + 1, current.Size, current.Assess, current.Normalize);
            Array.Copy(current.Entities, 0, next.Entities, 0, RestoreCount);
            int index = RestoreCount;
            Thread[] threads = new Thread[ThreadCount];
            int runningcount = ThreadCount;
            for(int i=0;i<ThreadCount;i++)
            {
                threads[i] = new(() =>
                {
                    while (index < current.Size)
                    {
                        current.SelectPair(out int fatherindex, out int motherindex);
                        Crossover.Crossover(current[fatherindex].Code, current[motherindex].Code, out CodeOL tson, out CodeOL tdaughter);
                        CodeOL son = Mutation.Mutation(tson);
                        CodeOL daughter = Mutation.Mutation(tdaughter);
                        Entity<CodeOL, int[]> entityson = new(son, Factory, current.Assess); 
                        Entity<CodeOL, int[]> entitydaughter = new(daughter, Factory, current.Assess);
                        lock (next)
                        {
                            if (index < current.Size)
                                next[index++] = entityson;
                            if (index < current.Size)
                                next[index++] = entitydaughter;
                        }
                    }
                    runningcount--;
                });
                threads[i].Start();
            }
            while (true)
            {
                bool t = true;
                for (int i = 0; i < ThreadCount; i++)
                    if (threads[i].IsAlive) t = false;
                if (t) break;
            }
            return next;
        }
    }
}
