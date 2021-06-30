using TestFramework;
namespace Code.Core.Test
{
    public class Hadamard_Test : ITest
    {
        public Hadamard_Test()
            : base("Hadamard_Test",9)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            for (int i = 1, j = 1; i <= 9; i++,j<<=1)
            {
                Hadamard hadamard = Hadamard.Get(j);
                Ensure.Equal(hadamard.ToString(), Hadamard.Print(j));
                update(i);
            }
        }
    }
}
