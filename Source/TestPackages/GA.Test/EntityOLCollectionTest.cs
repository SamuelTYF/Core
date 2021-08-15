using TestFramework;
using GA.OL;
namespace GA.Test
{
    public class EntityOLCollectionTest:ITest
    {
        public Assess<int[]> Assess;
        public NormalizeNE Normalize;
        public int[,] Values;
        public int Length;
        public static int GCD(int x, int y) => y == 0 ? x : GCD(y, x % y);
        public const int Count = 10;
        public EntityOLCollectionTest()
            :base("EntityOLCollectionTest",16)
        {
            Length = 1000;
            Values = new int[Length, Length];
            for (int i = 0; i < Length; i++)
                for (int j = 0; j < Length; j++)
                    Values[i, j] = GCD(i + 1, j + 1);
            Assess = (int[] xs) =>
            {
                double r = Values[xs[Length - 1], xs[0]];
                for (int i = 1; i < Length; i++)
                    r += Values[xs[i - 1], xs[i]];
                return r;
            };
            Normalize = new();
        }
        public override void Run(UpdateTaskProgress update)
        {
            CodeOLFactory factory = new(Length);
            EntityOLCollection collection = new(0, 16, Assess, Normalize);
            collection.Inital(factory);
            collection.Update();
            for(int i=0; i < 16;i++)
            {
                Ensure.Equal(collection.Get(collection.Possibilities[i]), i);
                update(i + 1);
            }
        }
    }
}
