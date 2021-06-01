using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace TestFramework
{
    public partial class TestForm : Form
    {
        public int SubTaskCount;
        public TestForm()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetEntryAssembly();
            Type ITest = typeof(ITest);
            SubTaskCount = 0;
            foreach (Type t in assembly.GetTypes())
                if (t.BaseType == ITest)
                {
                    ITest test = t.GetConstructor(Array.Empty<Type>()).Invoke(null) as ITest;
                    listView1.Items.Add(new ListViewItem(test.TestName)
                    {
                        SubItems = { "Prepared",$"0/{test.TaskCount}","" },
                        Tag = test
                    });
                    SubTaskCount += test.TaskCount;
                }
        }
        public void Run()
        {
            int ErrorCount = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = SubTaskCount;
            progressBar1.Minimum = 0;
            int value = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                ITest test = item.Tag as ITest;
                item.SubItems[1].Text = "Running";
                test.UpdateInfo = (object value) =>
                {
                    item.SubItems[3].Tag = value;
                    item.SubItems[3].Text = $"{value}";
                };
                int runningcount = 0;
                try
                {
                    test.Run((count) =>
                    {
                        item.SubItems[2].Text = $"{runningcount = count}/{test.TaskCount}";
                        progressBar1.Value = value + count;
                    });
                    item.SubItems[1].Text = "Success";
                }
                catch (Exception e)
                {
                    ErrorCount++;
                    item.SubItems[1].Text = "Fail";
                    item.SubItems[1].Tag = e;
                }
                value += test.TaskCount;
            }
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
            => backgroundWorker1.RunWorkerAsync();
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            runToolStripMenuItem.Enabled = false;
            Run();
            runToolStripMenuItem.Enabled = true;
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
            => Run();
        private void TestForm_SizeChanged(object sender, EventArgs e)=>TestName.Width= Result.Width=Progress.Width=Info.Width = Width / 4;
        private void TestForm_Load(object sender, EventArgs e) => TestName.Width = Result.Width = Progress.Width=Info.Width= Width / 4;
        private void exceptionToolStripMenuItem_Click(object sender, EventArgs e) => DebugForm.Display("Error", listView1.SelectedItems[0].SubItems[1].Tag);
        private void loadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Type ITest = typeof(ITest);
            Text = Environment.CurrentDirectory;
            progressBar1.Value = 0;
            SubTaskCount = 0;
            foreach (FileInfo fi in new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(fi.FullName);
                foreach (Type t in assembly.GetTypes())
                    if (t.BaseType == ITest)
                    {
                        ITest test = t.GetConstructor(Array.Empty<Type>()).Invoke(null) as ITest;
                        listView1.Items.Add(new ListViewItem(test.TestName)
                        {
                            SubItems = { "Prepared", $"0/{test.TaskCount}","" },
                            Tag = test
                        });
                        SubTaskCount += test.TaskCount;
                    }
            }
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e) => DebugForm.Display("Info", listView1.SelectedItems[0].SubItems[3].Tag);
    }
}
