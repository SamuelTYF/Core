using CSharpScript.DomTree;
using CSharpScript.File;
using System;
using System.IO;
using TestFramework;

namespace CSharpScript.Test
{
    public class ILBody_Test:ITest
    {
        public ILBody_Test()
            :base("ILBody_Test",2)
        {
        }
        public void Try()
        {
            try
            {
                int t = int.Parse("123");
                Console.WriteLine(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public override void Run(UpdateTaskProgress update)
        {
            PEManager manager = new();
            PEFile file = manager.Load("CSharpScript.Test.dll");
            update(1);
            TypeDef type = file.GetType("ILBody_Test");
            Ensure.NotNull(type);
            MethodDef method = type.GetMethod("Try")[0];
            ILMethod ilmethod=ILCode.Parse(method);
            UpdateInfo(ilmethod.Print());
        }
    }
}
