using System;

namespace GA.SD
{
    public class Entity64Collection : IEntityCollection<Code64, double>
    {
        public int Generation { get; private set; }
        public int Size { get; private set; }
        public Entity<Code64, double>[] Entities { get; private set; }
        public Assess<double> Assess { get; private set; }
        public INormalize Normalize { get; private set; }
        public double[] Possibilities;
        public double PossibilitySum;
        public Entity<Code64, double> this[int index]
        {
            get => Entities[index];
            set => Entities[index] = value;
        }
        public Entity64Collection(int generation,int size,Assess<double> assess, INormalize normalize)
        {
            Generation= generation;
            Size=size;
            Entities = new Entity<Code64,double>[size];
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
                PossibilitySum += Possibilities[i] = Normalize.Normalize(Entities[i].Score);
        }
        public int SelectSingle()
        {
            double r = PossibilitySum * RandomHelper.NextDouble();
            int i = 0;
            while (r > Possibilities[i]) r -= Possibilities[i++];
            return i;
        }
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
        public void Inital(ICodeFactory<Code64, double> factory)
        {
            for (int i = 0; i < Size; i++)
            {
                factory.Random(out Code64 code, out double value);
                Entities[i] = new(code, value, Assess(value));
            }
        }
    }
}
