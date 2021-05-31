namespace Automata
{
    public sealed class AKC
    {
        public static readonly AutomataInstance AKC_Instance;
        static AKC()
        {
            AutomataKernel automataKernel = new();
            automataKernel.SetInputCount(256);
            automataKernel.RegisterMode("Stop");
            automataKernel.RegisterMode("Name");
            automataKernel.RegisterMode("LeftBracket");
            automataKernel.RegisterMode("Value");
            automataKernel.RegisterMode("Comma");
            automataKernel.RegisterMode("RightBracket");
            automataKernel.LockModes();
            automataKernel.RegisterFunction("Throw");
            automataKernel.RegisterFunction("Init");
            automataKernel.RegisterFunction("Push");
            automataKernel.RegisterFunction("Store");
            automataKernel.RegisterFunction("Tackle");
            automataKernel.RegisterFunction("Build");
            automataKernel.SetInitFunction("Init");
            automataKernel.RegisterRule("Stop", AutomataKernel.English, "Push", "Name");
            automataKernel.RegisterRule("Name", AutomataKernel.English, "Push", "Name");
            automataKernel.RegisterRule("Name", 40, "Store", "LeftBracket");
            automataKernel.RegisterRule("LeftBracket", AutomataKernel.English, "Push", "Value");
            automataKernel.RegisterRule("LeftBracket", AutomataKernel.Number, "Push", "Value");
            automataKernel.RegisterRule("LeftBracket", 41, "Tackle", "RightBracket");
            automataKernel.RegisterRule("Value", AutomataKernel.English, "Push", "Value");
            automataKernel.RegisterRule("Value", AutomataKernel.Number, "Push", "Value");
            automataKernel.RegisterRule("Value", 44, "Store", "Comma");
            automataKernel.RegisterRule("Comma", AutomataKernel.English, "Push", "Value");
            automataKernel.RegisterRule("Comma", AutomataKernel.Number, "Push", "Value");
            automataKernel.RegisterRule("Value", 41, "Tackle", "RightBracket");
            automataKernel.RegisterRule("RightBracket", new int[2] { 13, 10 }, null, "RightBracket");
            automataKernel.RegisterRule("RightBracket", AutomataKernel.English, "Push", "Name");
            automataKernel.RegisterRule("RightBracket", 0, "Build", "Stop");
            automataKernel.SetEndCheck(true);
            AKC_Instance = automataKernel.CreateInstance();
        }
        public static AutomataInstance ReadFrom(string source)
        {
            AKCHost host = new();
            AKC_Instance.Run(host, source, 0, source.Length);
            return host.Result;
        }
        public static AutomataInstance ReadFrom<TStringArg>(TStringArg source) where TStringArg : IStringArg
        {
            AKCHost host = new();
            AKC_Instance.Run(host, source);
            return host.Result;
        }
    }
}
