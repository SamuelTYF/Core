using Automata.IDE.Properties;
using Collection;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace Automata.IDE
{
    public partial class IDEForm : Form
    {
        public int Curve;
        private static readonly string[] keys = new string[8] { "SetInputCount(256)", "RegisterMode()", "LockModes()", "RegisterFunction()", "SetInitFunction()", "RegisterRule(,,,)", "RegisterRule(,,,,)", "SetEndCheck(true)" };
        private static readonly string[] values = new string[3] { "English", "Number", "Sign" };
        public AutomataInstance Instance;
        public Type HostType;
        public string[] Formats = new string[2]
        {
            DataFormats.FileDrop,
            DataFormats.Text
        };
        public Debugger Debugger;
        public bool Debugging;
        public bool SourceOn = true;
        public bool TextOn = true;
        public bool StateOn = true;
        public IDEForm(string path = null)
        {
            InitializeComponent();
            Open(path);
        }
        public void Open(string path)
        {
            try
            {
                if (path == null) return;
                if (path.EndsWith(".ams"))
                {
                    using (StreamReader streamReader = new(path))
                        AMSBox.Text = streamReader.ReadToEnd();
                    checkToolStripMenuItem.PerformClick();
                }
                else if (path.EndsWith(".amss"))
                {
                    using StreamReader streamReader2 = new(path);
                    AMSSBox.Text = streamReader2.ReadToEnd();
                }
                else
                {
                    if (!path.EndsWith(".ami")) return;
                    using StreamReader sr = new(path);
                    string dir = new FileInfo(path).Directory.FullName + "\\";
                    string path2 = sr.ReadLine();
                    if (path2.StartsWith("."))
                        path2 = dir + path2;
                    string path3 = sr.ReadLine();
                    if (path3.StartsWith("."))
                        path3 = dir + path3;
                    string path4 = sr.ReadLine();
                    if (path4.StartsWith("."))
                        path4 = dir + path4;
                    string host = sr.EndOfStream ? null : sr.ReadLine();
                    using (StreamReader streamReader3 = new(path2))
                        AMSBox.Text = streamReader3.ReadToEnd();
                    checkToolStripMenuItem.PerformClick();
                    using (StreamReader srr = new(path3))
                        AMSSBox.Text = srr.ReadToEnd();
                    Assembly assembly = Assembly.LoadFrom(path4);
                    HostBox.Items.Clear();
                    int index = 0;
                    Type[] exportedTypes = assembly.GetExportedTypes();
                    foreach (Type type in exportedTypes)
                        if (typeof(AutomataHost).FullName.Equals(type.BaseType?.FullName) && (type.GetConstructor(Type.EmptyTypes) != null || type.GetConstructor(new Type[1] { typeof(IStringArg) }) != null))
                        {
                            HostBox.Items.Add(type);
                            if (type.FullName.CompareTo(host) == 0)
                                index = HostBox.Items.Count - 1;
                        }
                    if (host != null)
                        HostBox.SelectedIndex = index;
                    else if (HostBox.Items.Count == 1)
                        HostBox.SelectedIndex = 0;
                    return;
                }
            }
            catch (Exception e)
            {
                ConsoleBox.AppendText($"{DateTime.Now}:{e}");
            }
        }
        public void UpdateTool(List<string> modes, List<string> functions)
        {
            contextmenu.Items.Clear();
            string[] array = keys;
            foreach (string key in array)
            {
                ToolStripMenuItem Key = new(key);
                Key.Click += delegate
                {
                    AMSBox.SelectedText = key;
                };
                contextmenu.Items.Add(Key);
            }
            foreach (string mode in modes)
            {
                ToolStripMenuItem Mode = new(mode);
                Mode.Click += delegate
                {
                    AMSBox.SelectedText = mode;
                };
                contextmenu.Items.Add(Mode);
            }
            array = values;
            foreach (string value in array)
            {
                ToolStripMenuItem Value = new(value);
                Value.Click += delegate
                {
                    AMSBox.SelectedText = value;
                };
                contextmenu.Items.Add(Value);
            }
            foreach (string function in functions)
            {
                ToolStripMenuItem Function = new(function);
                Function.Click += delegate
                {
                    AMSBox.SelectedText = function;
                };
                contextmenu.Items.Add(Function);
            }
        }
        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMSBox.SuspendLayout();
            RichTextStringArg arg = new(AMSBox, SourceOn);
            AKCHost host = new(arg);
            try
            {
                AKC.AKC_Instance.DebugRun(host, arg);
                createInstanceToolStripMenuItem.Enabled = true;
            }
            catch (Exception ee)
            {
                ConsoleBox.AppendText($"{DateTime.Now}:{ee}");
                createInstanceToolStripMenuItem.Enabled = false;
            }
            finally
            {
                InfoBox.Clear();
                foreach (var (error, index, _) in host.Errors)
                {
                    InfoBox.AppendText($"{index}:{error}\n");
                }
                AMSBox.Select(Curve, 0);
                UpdateTool(host._Modes, host._Functions);
            }
            AMSBox.PerformLayout();
        }
        private void createInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Instance = Automata.AKC.ReadFrom(AMSBox.Text);
                AMSBox.Enabled = false;
                disposeToolStripMenuItem.Enabled = true;
                createInstanceToolStripMenuItem.Enabled = false;
                checkToolStripMenuItem.Enabled = false;
                fileToolStripMenuItem.Enabled = false;
            }
            catch (Exception ee)
            {
                ConsoleBox.AppendText($"{DateTime.Now}:{ee}");
            }
        }
        private void disposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMSBox.Enabled = true;
            Instance = null;
            AMSBox.Enabled = true;
            disposeToolStripMenuItem.Enabled = false;
            createInstanceToolStripMenuItem.Enabled = true;
            checkToolStripMenuItem.Enabled = true;
            fileToolStripMenuItem.Enabled = true;
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HostDialog.ShowDialog() != DialogResult.OK)
                return;
            HostDialog.OpenFile().Dispose();
            Assembly assembly = Assembly.LoadFrom(HostDialog.FileName);
            HostBox.Items.Clear();
            Type[] exportedTypes = assembly.GetExportedTypes();
            foreach (Type type in exportedTypes)
            {
                if (typeof(AutomataHost).FullName.Equals(type.BaseType?.FullName) && (type.GetConstructor(Type.EmptyTypes) != null || type.GetConstructor(new Type[1] { typeof(IStringArg) }) != null))
                    HostBox.Items.Add(type);
            }
            if (HostBox.Items.Count == 1)
                HostBox.SelectedIndex = 0;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e) => AMSBox.Clear();
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AMIDialog.ShowDialog() != DialogResult.OK)return;
            string path = AMIDialog.FileName;
            Open(path);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                SaveDialog.OpenFile().Dispose();
                FileInfo fi = new(SaveDialog.FileName);
                if (fi.Exists)fi.Delete();
                using StreamWriter sw = new(fi.OpenWrite());
                sw.Write(AMSBox.Text);
            }
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HostType = HostBox.SelectedItem as Type;
            ConsoleBox.AppendText($"{DateTime.Now}:{HostType}\n");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AMSSBox.DragDrop += RichTextBox2_DragDrop;
            AMSSBox.DragEnter += RichTextBox2_DragEnter;
            if (!Settings.Default.SourceOn)
                sourceOnToolStripMenuItem.PerformClick();
            if (!Settings.Default.TextOn)
                textOnToolStripMenuItem.PerformClick();
            if (!Settings.Default.StateOn)
                stateOnToolStripMenuItem.PerformClick();
            UpdateTool(new List<string>(), new List<string>());
        }
        private void RichTextBox2_DragEnter(object sender, DragEventArgs e) => e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        private void RichTextBox2_DragDrop(object sender, DragEventArgs e)
        {
            using (StreamReader sr = new(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0) as string))
                AMSSBox.Text = sr.ReadToEnd();
            e.Effect = DragDropEffects.None;
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextStringArg arg = new(AMSSBox, SourceOn);
            try
            {
                AutomataHost host = (AutomataHost)Activator.CreateInstance(HostType);
                Instance.Run(host, arg);
            }
            catch (Exception ee)
            {
                ConsoleBox.AppendText($"{DateTime.Now}:{ee}\n");
            }
            finally
            {
                AMSSBox.Select(arg.StringArg.index, arg.StringArg.end - arg.StringArg.index);
                AMSSBox.SelectionColor = Color.Red;
                AMSSBox.Select(arg.StringArg.index, 0);
            }
        }
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            List<int> numbers = new();
            for (int i = 0; i < StringBox.Text.Length; i++)
            {
                if (StringBox.Text[i] == '\\')
                {
                    switch (StringBox.Text[++i])
                    {
                        case '\\':
                            numbers.Add(92);
                            break;
                        case 'n':
                            numbers.Add(10);
                            break;
                        case '\r':
                            numbers.Add(13);
                            break;
                    }
                }
                else
                    numbers.Add(StringBox.Text[i]);
            }
            CharBox.Text = string.Join(",", numbers.ToArray());
        }
        public void UpdateDebugging(bool debugging)
        {
            ToolStripMenuItem toolStripMenuItem = continueToolStripMenuItem;
            bool flag2 = (moveToNextToolStripMenuItem.Enabled = debugging);
            bool debugging2 = (toolStripMenuItem.Enabled = flag2);
            Debugging = debugging2;
        }
        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debugging = true;
            Debugger = new Debugger(AMSBox, AMSSBox, HostType, delegate (string s)
            {
                InfoBox.Text = s;
                if (StateOn)
                    InfoBox.Refresh();
            }, SourceOn, () => TextOn);
            if (Debugger.BeginDebug())
                UpdateDebugging(debugging: true);
            else
                UpdateDebugging(debugging: false);
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutomataHost host = ((!(HostType.GetConstructor(new Type[1] { typeof(IStringArg) }) != null)) ? ((AutomataHost)Activator.CreateInstance(HostType)) : ((AutomataHost)Activator.CreateInstance(HostType, new object[1])));
            InfoBox.Clear();
            InfoBox.AppendText($"{host}\n");
            MethodInfo[] mis = host.GetType().GetMethods();
            int[] functionHash = host.FunctionHash;
            foreach (int i in functionHash)
            {
                MethodInfo[] array = mis;
                foreach (MethodInfo mi in array)
                {
                    if (i == mi.Name.GetHashCode())
                        InfoBox.AppendText(mi.Name + "\n");
                }
            }
        }
        private void moveToNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.MoveToNext();
            UpdateDebugging(Debugger.Debugging);
        }
        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debugger.Continue();
            UpdateDebugging(Debugger.Debugging);
        }
        private void breakPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = AMSSBox.SelectionStart;
            int end = AMSSBox.SelectionStart + AMSSBox.SelectionLength;
            if (end == index)
                end++;
            for (int i = index; i < end; i++)
            {
                AMSSBox.Select(i, 1);
                AMSSBox.SelectionBackColor = ((AMSSBox.SelectionBackColor == Color.White) ? Color.Red : Color.White);
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e) => UpdateDebugging(Debugger.Debugging = false);
        private void sourceOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceOn = !SourceOn;
            sourceOnToolStripMenuItem.Text = "Source " + (SourceOn ? "On" : "Off");
        }
        private void textOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextOn = !TextOn;
            textOnToolStripMenuItem.Text = "Text " + (TextOn ? "On" : "Off");
        }
        private void stateOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StateOn = !StateOn;
            stateOnToolStripMenuItem.Text = "State " + (StateOn ? "On" : "Off");
        }
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "SetInputCount(256)\nRegisterMode(Stop)\nLockModes()\n";
            AutomataHost host = ((!(HostType.GetConstructor(new Type[1] { typeof(IStringArg) }) != null)) ? ((AutomataHost)Activator.CreateInstance(HostType)) : ((AutomataHost)Activator.CreateInstance(HostType, new object[1])));
            MethodInfo[] mis = host.GetType().GetMethods();
            int[] functionHash = host.FunctionHash;
            foreach (int i in functionHash)
            {
                MethodInfo[] array = mis;
                foreach (MethodInfo mi in array)
                {
                    if (i == mi.Name.GetHashCode())
                        s = s + "RegisterFunction(" + mi.Name + ")\n";
                }
            }
            s += "SetInitFunction(Init)\nSetEndCheck(true)";
            AMSBox.Text = s;
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Debugging && e.KeyData == Keys.Return)
            {
                Curve = AMSBox.SelectionStart;
                checkToolStripMenuItem.PerformClick();
            }
        }

        private void IDEForm_SizeChanged(object sender, EventArgs e)
        {
            AMSBox.Width =Width / 2;
            InfoBox.Width=ConsoleBox.Width = Width / 4;
            AMSBox.Height = Height;
            AMSSBox.Width = Width/2;
            AMSSBox.Height = InfoBox.Height = ConsoleBox.Height = Height / 2;
            AMSSBox.Left=InfoBox.Left = AMSBox.Width;
            ConsoleBox.Left = InfoBox.Width + InfoBox.Left;
        }
    }
}
