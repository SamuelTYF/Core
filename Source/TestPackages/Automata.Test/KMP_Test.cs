using TestFramework;

namespace Automata.Test
{
    public class KMP_Test:ITest
    {
        public KMP_Test()
            :base("KMP_Test",2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            KMP kmp = new("1111");
            int r = kmp.Run("111122111121");
        }
    }
}
