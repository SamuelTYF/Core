namespace GA.SD
{
    public class Evolution64 : IEvolution<Code64, double, Entity64Collection>
    {
        public int RestoreCount;
        public Code64Factory Factory;
        public Crossover64 Crossover;
        public Mutation64 Mutation;
        public Evolution64(int restorecount,Code64Factory factory, Crossover64 crossover,Mutation64 mutation)
        {
            RestoreCount = restorecount;
            Factory = factory;
            Crossover= crossover;
            Mutation= mutation;
        }
        public Entity64Collection Evolve(Entity64Collection current)
        {
            current.Update();
            Entity64Collection next = new(current.Generation + 1, current.Size, current.Assess, current.Normalize);
            int index = 0;
            while (index < RestoreCount )
                next[index] = current[index++];
            while (index < current.Size)
            {
                current.SelectPair(out int fatherindex, out int motherindex);
                Crossover.Crossover(current[fatherindex].Code, current[motherindex].Code, out Code64 tson, out Code64 tdaughter);
                Code64 son=Mutation.Mutation(tson);
                Code64 daughter=Mutation.Mutation(tdaughter);
                next[index++] = new(son, Factory, current.Assess);
                next[index++] = new(daughter, Factory, current.Assess);
            }
            return next;
        }
    }
}
