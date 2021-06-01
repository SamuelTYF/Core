using System;
namespace Automata.IDE
{
    public static class AKC
    {
        public static readonly AutomataInstance AKC_Instance;
        static AKC()
        {
            AutomataKernel kernel = new();
            kernel.SetInputCount(256);
            kernel.RegisterMode("Stop");
            kernel.RegisterMode("Error");
            kernel.RegisterMode("RightBracket");
            kernel.RegisterMode("Name");
            kernel.RegisterMode("LeftBracket");
            kernel.RegisterMode("Value");
            kernel.RegisterMode("Comma");
            kernel.LockModes();
            kernel.RegisterFunction("Throw");
            kernel.RegisterFunction("Init");
            kernel.RegisterFunction("Push");
            kernel.RegisterFunction("Store");
            kernel.RegisterFunction("Tackle");
            kernel.RegisterFunction("Build");
            kernel.RegisterFunction("SignedError");
            kernel.SetInitFunction("Init");
            kernel.RegisterRule("Stop", AutomataKernel.English, "Push", "Name");
            kernel.RegisterRule("Name", AutomataKernel.English, "Push", "Name");
            kernel.RegisterRule("Name", 40, "Store", "LeftBracket");
            kernel.RegisterRule("LeftBracket", AutomataKernel.English, "Push", "Value");
            kernel.RegisterRule("LeftBracket", AutomataKernel.Number, "Push", "Value");
            kernel.RegisterRule("LeftBracket", 41, "Tackle", "RightBracket");
            kernel.RegisterRule("Value", AutomataKernel.English, "Push", "Value");
            kernel.RegisterRule("Value", AutomataKernel.Number, "Push", "Value");
            kernel.RegisterRule("Value", 44, "Store", "Comma");
            kernel.RegisterRule("Comma", AutomataKernel.English, "Push", "Value");
            kernel.RegisterRule("Comma", AutomataKernel.Number, "Push", "Value");
            kernel.RegisterRule("Value", 41, "Tackle", "RightBracket");
            kernel.RegisterRule("RightBracket", new int[2] { 13, 10 }, null, "RightBracket");
            kernel.RegisterRule("RightBracket", AutomataKernel.English, "Push", "Name");
            kernel.RegisterRule("RightBracket", 0, "Build", "Stop");
            kernel.RegisterRule("Error", AutomataKernel.English, null, "Error");
            kernel.RegisterRule("Error", AutomataKernel.Number, null, "Error");
            kernel.RegisterRule("Error", new int[4] { 40, 44, 13, 10 }, null, "Error");
            kernel.RegisterRule("Error", 41, "SignedError", "RightBracket");
            kernel.RegisterRule("Error", 0, "SignedError", "Stop");
            kernel.SetEndCheck(tf: true);
            AKC_Instance = kernel.CreateInstance();
        }
        public static void DebugRun<TStringArg>(this AutomataInstance instance, AKCHost host, TStringArg source) where TStringArg : IStringArg
        {
            if (!host.CheckFunctions(instance.Functions))
                throw new Exception();
            AutomataInstance.RunFunction(host, instance.InitFunction);
            int mode = 0;
            int tmode = 0;
            bool t = false;
            host.ChangeMode = delegate (int Mode)
            {
                tmode = Mode;
                t = true;
            };
            while (source.NotOver)
            {
                t = false;
                char input = (char)(host.Input = source.Top());
                mode = instance.Run(host, mode + (host.Input << 1));
                if (t)
                    mode = tmode;
                if (mode == 0)
                    break;
                source.Pop();
            }
            if (instance.EndCheck && mode != 0 && instance.Run(host, mode) != 0)
                throw new Exception();
        }
    }
}
