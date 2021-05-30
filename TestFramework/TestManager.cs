using System;
using System.Reflection;
using static System.Console;
namespace TestFramework
{
    public class TestManager
    {
        public static void Run()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            int Count = 0;
            int ErrorCount = 0;
            Type ITest = typeof(ITest);
            foreach(Type t in assembly.GetTypes())
                if(t.BaseType== ITest)
                {
                    Count++;
                    ITest test = t.GetConstructor(new Type[0]).Invoke(null) as ITest;
                    WriteLine(Count);
                    WriteLine(test.TestName);
                    try
                    {
                        test.Run();
                    }
                    catch(Exception e)
                    {
                        ErrorCount++;
                        WriteLine(e);
                    }
                }
            WriteLine($"Success:{Count-ErrorCount}\nError:{ErrorCount}\nTotal:{Count}");
            ReadLine();
        }
    }
}
