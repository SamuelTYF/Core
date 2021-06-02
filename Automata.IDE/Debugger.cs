using Collection;
using System;
using System.Windows.Forms;
namespace Automata.IDE
{
    public class Debugger
    {
        public RichTextStringArg Source;
        public DebuggerStringArg Text;
        public Action<string> Display;
        public Type HostType;
        public AutomataHost Host;
        public AutomataInstance Instance;
        public string[] Modes;
        public string[] Functions;
        public int Index;
        public bool NotOver;
        public int Count;
        public int ModeCount;
        public int Mode;
        public string ModeName;
        public int Input;
        public int Offset;
        public string Function;
        public TrieTree<(int, int)?[]> Rules;
        public bool Debugging;
        public bool Break;
        public Debugger(RichTextBox source, RichTextBox text, Type host, Action<string> display, bool sourceon, Func<bool> textOn)
        {
            Source = new RichTextStringArg(source, sourceon);
            Text = new DebuggerStringArg(text, textOn);
            HostType = host;
            Display = display;
        }
        public void Show() => Display($"Index:{Index}\n" + $"NotOver:{NotOver}\n" + $"Count:{Count}\n" + $"ModeCount:{ModeCount}\n" + $"Mode:{Mode}\n" + "ModeName:" + ModeName + "\n" + $"Input:{Input}\n" + $"Offset:{Offset}\n" + "Function:" + Function + "\n");
        public bool BeginDebug()
        {
            if (Debugging)
                return false;
            Break = false;
            try
            {
                AKCHost host = new(Source);
                AKC.AKC_Instance.DebugRun(host, Source);
                Instance = host.Result;
                Modes = host._Modes.ToArray();
                Functions = new string[host._Functions.Length - 1];
                for (int i = 1; i < host._Functions.Length; i++)
                    Functions[i - 1] = host._Functions[i];
                Rules = host.Rules;
                Host = HostType.GetConstructor(new Type[1] { typeof(IStringArg) }) != null
                    ? (AutomataHost)Activator.CreateInstance(HostType, Text)
                    : (AutomataHost)Activator.CreateInstance(HostType);
                if (!Host.CheckFunctions(Instance.Functions))
                {
                    string r = "";
                    string[] functions = Functions;
                    foreach (string s in functions)
                        if (host.GetType().GetMethod(s, Type.EmptyTypes) == null)
                            r = r + "Host Hasn't Define " + s + "\n";
                    Display(r);
                    return false;
                }
                if (!Text.NotOver)
                {
                    Display("Text Can't be Empty");
                    return false;
                }
                AutomataInstance.RunFunction(Host, Instance.InitFunction);
                Count = Instance.InputCount;
                ModeCount = Instance.ModeCount;
                Mode = 0;
                ModeName = Modes[Mode / Count >> 1];
                Input = Text.Top();
                NotOver = Text.NotOver;
                Index = Text.StringArg.index;
                Offset = Mode + (Input << 1);
                Function = ((Instance.Rules[Offset] == int.MaxValue) ? "null" : Functions[Instance.Rules[Offset]]);
                Show();
                (int, int)? region = Rules[ModeName, 0][Input];
                if (!region.HasValue)
                    return Debugging = false;
                Source.SetBackColor(region.Value);
                Debugging = true;
                return true;
            }
            catch (Exception e)
            {
                Display(e.ToString());
                return false;
            }
        }
        public bool MoveToNext()
        {
            if (!Debugging)
                return false;
            if (Break)
                Break = false;
            Mode = Instance.Run(Host, Offset);
            ModeName = Modes[Mode / Count >> 1];
            if (Mode == 0)
            {
                NotOver = Text.NotOver;
                Offset = 0;
                Function = "null";
                Show();
                Source.SetBackColor((0, 0));
                return Debugging = false;
            }
            Text.Pop();
            if (!Text.NotOver)
                Input = 0;
            else
            {
                (char, bool) top = Text.GetTop();
                Input = top.Item1;
                Break = top.Item2;
            }
            NotOver = Text.NotOver;
            Index = Text.StringArg.index;
            Offset = Mode + (Input << 1);
            Function = ((Instance.Rules[Offset] == int.MaxValue) ? "null" : Functions[Instance.Rules[Offset]]);
            Show();
            (int, int)? region = Rules[ModeName, 0][Input];
            if (!region.HasValue)
                return Debugging = false;
            Source.SetBackColor(region.Value);
            return true;
        }
        public bool Continue()
        {
            while (Debugging && !Break)
                MoveToNext();
            if (Break)
                Break = false;
            else
                Debugging = false;
            return true;
        }
    }
}
