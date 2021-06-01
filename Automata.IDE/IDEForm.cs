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
                        richTextBox1.Text = streamReader.ReadToEnd();
                    checkToolStripMenuItem.PerformClick();
                }
                else if (path.EndsWith(".amss"))
                {
                    using StreamReader streamReader2 = new(path);
                    richTextBox2.Text = streamReader2.ReadToEnd();
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
                        richTextBox1.Text = streamReader3.ReadToEnd();
                    checkToolStripMenuItem.PerformClick();
                    using (StreamReader srr = new(path3))
                        richTextBox2.Text = srr.ReadToEnd();
                    Assembly assembly = Assembly.LoadFrom(path4);
                    toolStripComboBox1.Items.Clear();
                    int index = 0;
                    Type[] exportedTypes = assembly.GetExportedTypes();
                    foreach (Type type in exportedTypes)
                    {
                        if (typeof(AutomataHost).FullName.Equals(type.BaseType?.FullName) && (type.GetConstructor(Type.EmptyTypes) != null || type.GetConstructor(new Type[1] { typeof(IStringArg) }) != null))
                        {
                            toolStripComboBox1.Items.Add(type);
                            if (type.FullName.CompareTo(host) == 0)
                                index = toolStripComboBox1.Items.Count - 1;
                        }
                    }
                    if (host != null)
                        toolStripComboBox1.SelectedIndex = index;
                    else if (toolStripComboBox1.Items.Count == 1)
                    {
                        toolStripComboBox1.SelectedIndex = 0;
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                richTextBox4.AppendText($"{DateTime.Now}:{e}");
            }
        }
        public void UpdateTool(List<string> modes, List<string> functions)
        {
            contextMenuStrip1.Items.Clear();
            string[] array = keys;
            foreach (string key in array)
            {
                ToolStripMenuItem Key = new(key);
                Key.Click += delegate
                {
                    richTextBox1.SelectedText = key;
                };
                contextMenuStrip1.Items.Add(Key);
            }
            foreach (string mode in modes)
            {
                ToolStripMenuItem Mode = new(mode);
                Mode.Click += delegate
                {
                    richTextBox1.SelectedText = mode;
                };
                contextMenuStrip1.Items.Add(Mode);
            }
            array = values;
            foreach (string value in array)
            {
                ToolStripMenuItem Value = new(value);
                Value.Click += delegate
                {
                    richTextBox1.SelectedText = value;
                };
                contextMenuStrip1.Items.Add(Value);
            }
            foreach (string function in functions)
            {
                ToolStripMenuItem Function = new(function);
                Function.Click += delegate
                {
                    richTextBox1.SelectedText = function;
                };
                contextMenuStrip1.Items.Add(Function);
            }
        }
        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SuspendLayout();
            RichTextStringArg arg = new(richTextBox1, SourceOn);
            AKCHost host = new(arg);
            try
            {
                AKC.AKC_Instance.DebugRun(host, arg);
                createInstanceToolStripMenuItem.Enabled = true;
            }
            catch (Exception ee)
            {
                richTextBox4.AppendText($"{DateTime.Now}:{ee}");
                createInstanceToolStripMenuItem.Enabled = false;
            }
            finally
            {
                richTextBox3.Clear();
                foreach (var (error, index, _) in host.Errors)
                {
                    richTextBox3.AppendText($"{index}:{error}\n");
                }
                richTextBox1.Select(Curve, 0);
                UpdateTool(host._Modes, host._Functions);
            }
            richTextBox1.PerformLayout();
        }
        private void createInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Instance = Automata.AKC.ReadFrom(richTextBox1.Text);
                richTextBox1.Enabled = false;
                disposeToolStripMenuItem.Enabled = true;
                createInstanceToolStripMenuItem.Enabled = false;
                checkToolStripMenuItem.Enabled = false;
                fileToolStripMenuItem.Enabled = false;
            }
            catch (Exception ee)
            {
                richTextBox4.AppendText($"{DateTime.Now}:{ee}");
            }
        }
        private void disposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Enabled = true;
            Instance = null;
            richTextBox1.Enabled = true;
            disposeToolStripMenuItem.Enabled = false;
            createInstanceToolStripMenuItem.Enabled = true;
            checkToolStripMenuItem.Enabled = true;
            fileToolStripMenuItem.Enabled = true;
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            openFileDialog1.OpenFile().Dispose();
            Assembly assembly = Assembly.LoadFrom(openFileDialog1.FileName);
            toolStripComboBox1.Items.Clear();
            Type[] exportedTypes = assembly.GetExportedTypes();
            foreach (Type type in exportedTypes)
            {
                if (typeof(AutomataHost).FullName.Equals(type.BaseType?.FullName) && (type.GetConstructor(Type.EmptyTypes) != null || type.GetConstructor(new Type[1] { typeof(IStringArg) }) != null))
                    toolStripComboBox1.Items.Add(type);
            }
            if (toolStripComboBox1.Items.Count == 1)
                toolStripComboBox1.SelectedIndex = 0;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Clear();
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog2.FileName;
            Open(path);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.OpenFile().Dispose();
                FileInfo fi = new(saveFileDialog1.FileName);
                if (fi.Exists)
                    fi.Delete();
                using StreamWriter sw = new(fi.OpenWrite());
                sw.Write(richTextBox1.Text);
            }
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HostType = toolStripComboBox1.SelectedItem as Type;
            richTextBox4.AppendText($"{DateTime.Now}:{HostType}\n");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox2.DragDrop += RichTextBox2_DragDrop;
            richTextBox2.DragEnter += RichTextBox2_DragEnter;
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
                richTextBox2.Text = sr.ReadToEnd();
            e.Effect = DragDropEffects.None;
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextStringArg arg = new(richTextBox2, SourceOn);
            try
            {
                AutomataHost host = (AutomataHost)Activator.CreateInstance(HostType);
                Instance.Run(host, arg);
            }
            catch (Exception ee)
            {
                richTextBox4.AppendText($"{DateTime.Now}:{ee}\n");
            }
            finally
            {
                richTextBox2.Select(arg.StringArg.index, arg.StringArg.end - arg.StringArg.index);
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.Select(arg.StringArg.index, 0);
            }
        }
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            List<int> numbers = new();
            for (int i = 0; i < toolStripTextBox1.Text.Length; i++)
            {
                if (toolStripTextBox1.Text[i] == '\\')
                {
                    switch (toolStripTextBox1.Text[++i])
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
                    numbers.Add(toolStripTextBox1.Text[i]);
            }
            toolStripTextBox2.Text = string.Join(",", numbers.ToArray());
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
            Debugger = new Debugger(richTextBox1, richTextBox2, HostType, delegate (string s)
            {
                richTextBox3.Text = s;
                if (StateOn)
                    richTextBox3.Refresh();
            }, SourceOn, () => TextOn);
            if (Debugger.BeginDebug())
                UpdateDebugging(debugging: true);
            else
                UpdateDebugging(debugging: false);
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutomataHost host = ((!(HostType.GetConstructor(new Type[1] { typeof(IStringArg) }) != null)) ? ((AutomataHost)Activator.CreateInstance(HostType)) : ((AutomataHost)Activator.CreateInstance(HostType, new object[1])));
            richTextBox3.Clear();
            richTextBox3.AppendText($"{host}\n");
            MethodInfo[] mis = host.GetType().GetMethods();
            int[] functionHash = host.FunctionHash;
            foreach (int i in functionHash)
            {
                MethodInfo[] array = mis;
                foreach (MethodInfo mi in array)
                {
                    if (i == mi.Name.GetHashCode())
                        richTextBox3.AppendText(mi.Name + "\n");
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
            int index = richTextBox2.SelectionStart;
            int end = richTextBox2.SelectionStart + richTextBox2.SelectionLength;
            if (end == index)
                end++;
            for (int i = index; i < end; i++)
            {
                richTextBox2.Select(i, 1);
                richTextBox2.SelectionBackColor = ((richTextBox2.SelectionBackColor == Color.White) ? Color.Red : Color.White);
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
            richTextBox1.Text = s;
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Debugging && e.KeyData == Keys.Return)
            {
                Curve = richTextBox1.SelectionStart;
                checkToolStripMenuItem.PerformClick();
            }
        }
    }
}
