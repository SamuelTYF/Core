using GA.OL;
using TestFramework;
namespace GA.Test
{
    public class CodeOLFactoryTest : ITest
    {
        public CodeOLFactoryTest()
            :base("CodeOLFactoryTest", 1000)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            for(int i=0;i<1000;i++)
            {
                CodeOLFactory cf = new(i+1);
                cf.Random(out CodeOL code, out int[] t);
                Ensure.Equal(cf.Decode(code), t);
                update(i+1);
            }
        }
    }
}
