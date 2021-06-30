using System;
using System.IO;
using Net.Json;
using TestFramework;
namespace Net.Test
{
    public class Json_Test : ITest
    {
        public DirectoryInfo Directory;
        public FileInfo[] Files;
        public Json_Test()
            :base("Json_Test",5)
        {
            Directory = new(Environment.CurrentDirectory);
            Files = Directory.GetFiles("*.json");
            TaskCount = Files.Length;
        }
        public override void Run(UpdateTaskProgress update)
        {
            for(int i=0;i<TaskCount;i++)
            {
                using StreamReader sr = new(Files[i].OpenRead());
                Node node = Node.Parse(sr.ReadToEnd());
                update(i + 1);
                UpdateInfo(node);
            }
        }
    }
}
