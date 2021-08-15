using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.SD;
using TestFramework;
namespace GA.Test
{
    public class Code64FactoryTest:ITest
    {
        public Code64FactoryTest()
            :base("Code64FactoryTest",1000)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Random r = new(DateTime.Now.Millisecond);
            for(int i=0;i<1000;i++)
            {
                Code64Factory cf = new(-1024*r.NextDouble(),1024*r.NextDouble());
                cf.Random(out Code64 code, out double t);
                Ensure.IsTrue(cf.IsLegal(t));
                Ensure.IsTrue(cf.IsLegal(code));
                Ensure.Equal(cf.Decode(code), t);
                Ensure.Equal(cf.Encode(t).Value, code.Value);
                update(i+1);
            }
        }
    }
}
