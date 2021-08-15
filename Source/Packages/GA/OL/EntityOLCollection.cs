using System;

namespace GA.OL
{
    public class EntityOLCollection : IEntityCollection<CodeOL, int[]>
    {
        public int Generation { get; private set; }
        public int Size { get; private set; }
        public Entity<CodeOL, int[]>[] Entities { get; private set; }
        public Assess<int[]> Assess { get; private set; }
        public INormalize Normalize { get; private set; }
        public double[] Possibilities;
        public double PossibilitySum;
        public Entity<CodeOL, int[]> this[int index]
        {
            get => Entities[index];
            set => Entities[index] = value;
        }
        public EntityOLCollection(int generation,int size,Assess<int[]> assess, INormalize normalize)
        {
            Generation= generation;
            Size=size;
            Entities = new Entity<CodeOL,int[]>[size];
            Assess= assess;
            Normalize = normalize;
        }
        public void Update()
        {
            Array.Sort(Entities);
            double max = Entities[^1].Score;
            double min = Entities[0].Score;
            Normalize.Inital(min, max);
            PossibilitySum = 0;
            Possibilities = new double[Size];
            for (int i = 0; i < Size; i++)
            {
                Possibilities[i] = PossibilitySum;
                PossibilitySum += Normalize.Normalize(Entities[i].Score);
            }
        }
        public int Get(double rate)
        {
            int l = 0, r = Size;
            while (l + 1 < r)
            {
                int m = (l + r) >> 1;
                if (rate < Possibilities[m]) r = m;
                else l = m;
            }
            return l;
        }
        public int SelectSingle() => Get(PossibilitySum * RandomHelper.NextDouble());
        public void SelectPair(out int father, out int mother)
        {
            father = SelectSingle();
            do mother = SelectSingle(); while (father == mother);
        }
        public void RandomSelectPair(out int father, out int mother)
        {
            father = RandomHelper.Next(Size);
            do mother = RandomHelper.Next(Size); while (father == mother);
        }
        public void Inital(ICodeFactory<CodeOL, int[]> factory)
        {
            for (int i = 0; i < Size; i++)
            {
                factory.Random(out CodeOL code, out int[] value);
                Entities[i] = new(code, value, Assess(value));
            }
        }
    }
}
