using System;
using System.Reflection;
using System.Windows.Forms;

namespace TestFramework
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetEntryAssembly();
            Type ITest = typeof(ITest);
            foreach (Type t in assembly.GetTypes())
                if (t.BaseType == ITest)
                {
                    ITest test = t.GetConstructor(new Type[0]).Invoke(null) as ITest;
                    listView1.Items.Add(new ListViewItem(test.TestName)
                    {
                        SubItems = { "Prepared" },
                        Tag=test
                    });
                }
        }
        public void Run()
        {
            int ErrorCount = 0;
            foreach(ListViewItem item in listView1.Items)
            {
                ITest test = item.Tag as ITest;
                item.SubItems[1].Text = "Running";
                try
                {
                    test.Run();
                    item.SubItems[1].Text="Success";
                }
                catch (Exception e)
                {
                    ErrorCount++;
                    item.SubItems[1].Text = "Fail";
                    item.SubItems[1].Tag = e;
                }
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

        private void TestForm_SizeChanged(object sender, EventArgs e)
        {
            TestName.Width = Width / 3;
            Result.Width = 2 * Width / 3;
        }
        private void TestForm_Load(object sender, EventArgs e)
        {
            TestName.Width = Width / 3;
            Result.Width = 2 * Width / 3;
        }

        private void exceptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugForm.Display("Error", listView1.SelectedItems[0].SubItems[1].Tag);
        }
    }
}
