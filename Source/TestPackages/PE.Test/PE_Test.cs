using PE;
using System.IO;
using TestFramework;

namespace PE.Test
{
    public class PE_Test:ITest
    {
        public PE_Test()
            :base("PE_Test",2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            using Stream stream = new FileInfo("ml64i4.dll").OpenRead();
            PEFile file=new(stream);
            UpdateInfo(file);
        }
    }
}
