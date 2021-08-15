using System.IO;

namespace TestFramework
{
    public abstract class ITest
    {
        public string TestName;
        public int TaskCount;
        public UpdateInfo UpdateInfo;
        public ITest(string name, int taskcount)
        {
            TestName = name;
            TaskCount = taskcount;
        }
        public abstract void Run(UpdateTaskProgress update);
        public void Save(object value,string file)
        {
            using StreamWriter sw = new(file);
            sw.WriteLine(value);
        }
    }
}
