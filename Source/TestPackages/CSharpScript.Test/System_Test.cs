using CSharpScript.File;
using System;
using System.IO;
using TestFramework;
namespace CSharpScript.Test
{
    public class File_Test:ITest
    {
        public FileInfo[] Files;
        public File_Test()
            :base("File_Test",1)
        {
            Files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.dll");
            TaskCount = Files.Length;
        }
        public void Try()
        {
            try
            {
                int t = int.Parse("123");
                Console.WriteLine(t);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public override void Run(UpdateTaskProgress update)
        {
            PEManager manager = new();
            for (int i=0;i<TaskCount;i++)
            {
                PEFile file = manager.Load(Files[i].Name);
                if (file == null) break;
                update(i + 1);
                UpdateInfo(file);
                using StreamWriter sw = new(Files[i].Name + ".txt");
                sw.WriteLine(file);
            }
        }
    }
}
