using TestFramework;
using CSharpScript.Resources;
using System;
using System.IO;

namespace CSharpScript.Test
{
    public class Resource_Test:ITest
    {
        public FileInfo[] Files;
        public Resource_Test()
            :base("Resource_Test",1)
        {
            Files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.resources");
            TaskCount = Files.Length;
        }
        public override void Run(UpdateTaskProgress update)
        {
            for (int i = 0; i < TaskCount; i++)
            {
                using FileStream fs = Files[i].OpenRead();
                ResourceReader rr = new(fs);
                update(i+1);
                UpdateInfo(rr);
            }
        }
    }
}
