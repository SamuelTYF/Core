using Collection;
using System.Drawing;
using System.Text;
namespace Automata.IDE
{
    public sealed class AKCHost : AutomataHost
    {
        public RichTextStringArg RichTextStringArg;
        public ChangeMode ChangeMode;
        public List<byte> TempBytes;
        public List<string> TempStrings;
        public List<int> Indexs;
        public AutomataKernel Kernel;
        public AutomataInstance Result;
        public Color Function = Color.DarkCyan;
        public Color Value = Color.Blue;
        public Color Mode = Color.DarkSeaGreen;
        public Color Sign = Color.FromArgb(206, 145, 120);
        public List<string> _Modes;
        public List<string> _Functions;
        public int State;
        public List<(string, int, int)> Errors;
        public TrieTree<(int, int)?[]> Rules;
        public int Size;
        public AKCHost(RichTextStringArg richTextStringArg) => RichTextStringArg = richTextStringArg;
        public void Init()
        {
            TempBytes = new();
            TempStrings = new();
            Indexs = new();
            Kernel = new();
            Result = null;
            _Modes = new();
            _Functions = new List<string>
            {
                "null"
            };
            State = 0;
            Errors = new();
            Rules = new();
        }
        public void Push() => TempBytes.Add((byte)Input);
        public void Store()
        {
            string s = Encoding.UTF8.GetString(TempBytes.ToArray());
            TempStrings.Add(s);
            Indexs.Add(RichTextStringArg.StringArg.index - s.Length);
            TempBytes.Clear();
        }
        public bool HasMode(string mode)
        {
            foreach (string mode2 in _Modes)
            {
                if (mode2 == mode)
                    return true;
            }
            return false;
        }
        public bool HasFunction(string function)
        {
            foreach (string function2 in _Functions)
            {
                if (function2 == function)
                    return true;
            }
            return false;
        }
        public void Tackle()
        {
            Store();
            switch (TempStrings[0])
            {
                case "SetInputCount":
                    if (TempStrings.Length != 2 || TempStrings[1] == "")
                        SetError("SetInputCount Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        int size = int.Parse(TempStrings[1]);
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Value);
                        Kernel.SetInputCount(size);
                        Size = size;
                    }
                    if (State != 0)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 1;
                    TempStrings.Clear();
                    break;
                case "RegisterMode":
                    if (TempStrings.Length != 2 || TempStrings[1] == "")
                        SetError("RegisterMode Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        string mode = TempStrings[1];
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Mode);
                        if (HasMode(mode))
                            SetError("Has Define " + mode, Indexs[1], TempStrings[1].Length);
                        else
                        {
                            _Modes.Add(mode);
                            Kernel.RegisterMode(mode);
                            Rules[mode, 0] = new (int, int)?[Size];
                        }
                    }
                    if (State != 1)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 1;
                    TempStrings.Clear();
                    break;
                case "LockModes":
                    if (TempStrings.Length != 2 || TempStrings[1] != "")
                        SetError("LockModes Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        Kernel.LockModes();
                    }
                    if (State != 1)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 2;
                    TempStrings.Clear();
                    break;
                case "RegisterFunction":
                    if (TempStrings.Length != 2 || TempStrings[1] == "")
                        SetError("RegisterFunction Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        string function = TempStrings[1];
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Sign);
                        if (HasFunction(function))
                            SetError("Has Define " + function, Indexs[1], TempStrings[1].Length);
                        else
                        {
                            Kernel.RegisterFunction(function);
                            _Functions.Add(function);
                        }
                    }
                    if (State != 2)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 2;
                    TempStrings.Clear();
                    break;
                case "SetInitFunction":
                    if (TempStrings.Length != 2 || TempStrings[1] == "")
                        SetError("SetInitFunction Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        string function2 = TempStrings[1];
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Sign);
                        if (!HasFunction(function2))
                            SetError("Hasn't Define " + function2, Indexs[1], TempStrings[1].Length);
                        else
                            Kernel.SetInitFunction(function2);
                    }
                    if (State != 2)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 3;
                    TempStrings.Clear();
                    break;
                case "RegisterRule":
                    if (TempStrings.Length == 5)
                    {
                        if (TempStrings[1] == "" || TempStrings[2] == "" || TempStrings[3] == "" || TempStrings[4] == "")
                            SetError("RegisterRule Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                        else
                        {
                            string mode3 = TempStrings[1];
                            string sindex = TempStrings[2];
                            string function4 = TempStrings[3];
                            string next2 = TempStrings[4];
                            RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                            RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Mode);
                            RichTextStringArg.SetColor(Indexs[2], TempStrings[2].Length, Value);
                            RichTextStringArg.SetColor(Indexs[3], TempStrings[3].Length, Sign);
                            RichTextStringArg.SetColor(Indexs[4], TempStrings[4].Length, Mode);
                            if (!HasMode(mode3))
                                SetError("Hasn't Define " + mode3, Indexs[1], TempStrings[1].Length);
                            else if (!HasFunction(function4))
                            {
                                SetError("Hasn't Define " + function4, Indexs[3], TempStrings[3].Length);
                            }
                            else if (!HasMode(next2))
                            {
                                SetError("Hasn't Define " + next2, Indexs[4], TempStrings[4].Length);
                            }
                            else if (int.TryParse(sindex, out int index2))
                            {
                                Kernel.RegisterRule(mode3, index2, function4, next2);
                                Rules[mode3, 0][index2] = (Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                            }
                            else
                            {
                                switch (sindex)
                                {
                                    case "Number":
                                        {
                                            Kernel.RegisterRule(mode3, AutomataKernel.Number, function4, next2);
                                            int[] sign = AutomataKernel.Number;
                                            foreach (int i in sign)
                                            {
                                                Rules[mode3, 0][i] = (Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                                            }
                                            break;
                                        }
                                    case "English":
                                        {
                                            Kernel.RegisterRule(mode3, AutomataKernel.English, function4, next2);
                                            int[] sign = AutomataKernel.English;
                                            foreach (int j in sign)
                                            {
                                                Rules[mode3, 0][j] = (Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                                            }
                                            break;
                                        }
                                    case "Sign":
                                        {
                                            Kernel.RegisterRule(mode3, AutomataKernel.Sign, function4, next2);
                                            int[] sign = AutomataKernel.Sign;
                                            foreach (int k in sign)
                                            {
                                                Rules[mode3, 0][k] = (Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                                            }
                                            break;
                                        }
                                    default:
                                        SetError("Can't Find " + sindex, Indexs[2], TempStrings[2].Length);
                                        break;
                                }
                            }
                        }
                    }
                    else if (TempStrings.Length == 6)
                    {
                        if (TempStrings[1] == "" || TempStrings[2] == "" || TempStrings[3] == "" || TempStrings[4] == "" || TempStrings[5] == "")
                            SetError("RegisterRule Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                        else
                        {
                            string mode2 = TempStrings[1];
                            int start = int.Parse(TempStrings[2]);
                            int end = int.Parse(TempStrings[3]);
                            string function3 = TempStrings[4];
                            string next = TempStrings[5];
                            RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                            RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Mode);
                            RichTextStringArg.SetColor(Indexs[2], TempStrings[2].Length, Value);
                            RichTextStringArg.SetColor(Indexs[3], TempStrings[3].Length, Value);
                            RichTextStringArg.SetColor(Indexs[4], TempStrings[4].Length, Sign);
                            RichTextStringArg.SetColor(Indexs[5], TempStrings[5].Length, Mode);
                            if (!HasMode(mode2))
                                SetError("Hasn't Define " + mode2, Indexs[1], TempStrings[1].Length);
                            else if (!HasFunction(function3))
                            {
                                SetError("Hasn't Define " + function3, Indexs[4], TempStrings[4].Length);
                            }
                            else if (!HasMode(next))
                            {
                                SetError("Hasn't Define " + next, Indexs[5], TempStrings[5].Length);
                            }
                            else
                            {
                                for (int index = start; index <= end; index++)
                                {
                                    Kernel.RegisterRule(mode2, index, function3, next);
                                    Rules[mode2, 0][index] = (Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                                }
                            }
                        }
                    }
                    else
                    {
                        SetError("RegisterRule Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    }
                    if (State != 3)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 3;
                    TempStrings.Clear();
                    break;
                case "SetEndCheck":
                    if (TempStrings.Length != 2 || TempStrings[1] == "")
                        SetError("SetEndCheck Format Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    else
                    {
                        bool tf = bool.Parse(TempStrings[1]);
                        RichTextStringArg.SetColor(Indexs[0], TempStrings[0].Length, Function);
                        RichTextStringArg.SetColor(Indexs[1], TempStrings[1].Length, Sign);
                        Kernel.SetEndCheck(tf);
                    }
                    if (State != 3)
                        SetError("State Error", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    State = 4;
                    TempStrings.Clear();
                    break;
                default:
                    SetError("Unknown Key", Indexs[0], RichTextStringArg.StringArg.index - Indexs[0]);
                    TempStrings.Clear();
                    break;
            }
            Indexs.Clear();
        }
        public void Build()
        {
            Result = Kernel.CreateInstance();
            RichTextStringArg.RichTextBox.Select(RichTextStringArg.StringArg.index, 0);
        }
        public void SetError(string s, int index, int length = 0)
        {
            Errors.Add((s, index, length));
            ChangeMode(512);
            if (length != 0)
            {
                SignedError();
                ChangeMode(1024);
            }
        }
        public void UpdateError(int length) => Errors[^1] = (Errors[^1].Item1, Errors[^1].Item2, length);
        public void SignedError()
        {
            bool num = Errors[^1].Item3 == 0;
            if (num)
                UpdateError(RichTextStringArg.StringArg.index - Errors[^1].Item2);
            RichTextStringArg.RichTextBox.Select(Errors[^1].Item2, Errors[^1].Item3);
            RichTextStringArg.RichTextBox.SelectionBackColor = Color.FromArgb(128, 255, 100, 100);
            if (num)
                Tackle();
        }
        public override HashTable<Function> MarkFunctions()
        {
            HashTable<Function> hashTable = new();
            hashTable.Register(delegate
            {
                SetError("Error", RichTextStringArg.StringArg.index);
            }, "Throw".GetHashCode());
            hashTable.Register(Init, "Init".GetHashCode());
            hashTable.Register(Push, "Push".GetHashCode());
            hashTable.Register(Store, "Store".GetHashCode());
            hashTable.Register(Tackle, "Tackle".GetHashCode());
            hashTable.Register(Build, "Build".GetHashCode());
            hashTable.Register(SignedError, "SignedError".GetHashCode());
            return hashTable;
        }
    }
}
