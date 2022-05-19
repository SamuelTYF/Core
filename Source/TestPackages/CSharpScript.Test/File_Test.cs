using CSharpScript.File;
using System;
using System.IO;
using TestFramework;
namespace CSharpScript.Test
{
    public class System_Test:ITest
    {
        public FileInfo File;
        public System_Test()
            :base("System_Test", 1)
        {
            File = new FileInfo(Environment.CurrentDirectory+"/Wolfram.dll");
        }
        public override void Run(UpdateTaskProgress update)
        {
            PEManager manager = new();
            PEFile file = manager.Load(File.FullName);
            update(1);
            UpdateInfo(file);
        }
    }
}
