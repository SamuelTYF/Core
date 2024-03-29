﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestFramework
{
    public partial class TestForm : Form
    {
        public int SubTaskCount;
        public bool RunningResult;
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
        public TestForm(string[] Args)
        {
            InitializeComponent();
            Text = string.Join(",",Args);
            backgroundWorker2.RunWorkerAsync();
        }
        public void Run()
        {
            int ErrorCount = 0;
            progressBar1.Invoke(() =>
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = SubTaskCount;
                progressBar1.Minimum = 0;
            });
            int value = 0;
            RunningResult = true;
            List<ListViewItem> items = new();
            listView1.Invoke(() => {
                foreach (ListViewItem item in listView1.Items)
                    items.Add(item);
            });
            foreach (ListViewItem item in items)
            {
                ITest test = item.Tag as ITest;
                listView1.Invoke(() =>
                {
                    item.SubItems[1].Text = "Running";
                    item.ForeColor = Color.Blue;
                });
                test.UpdateInfo =
                    TestBoxOn ? (object value) => richTextBox1.Invoke(()=> richTextBox1.AppendText($"{value}\n")):
                    (object value) =>
                    {
                        listView1.Invoke(() =>
                        {
                            item.SubItems[3].Tag = value;
                            item.SubItems[3].Text = $"{value}";
                        });
                    };
                int runningcount = 0;
                try
                {
                    test.Run((count) =>
                        listView1.Invoke(() =>
                        {
                            item.SubItems[2].Text = $"{runningcount = count}/{test.TaskCount}";
                            progressBar1.Value = value + count;
                        })
                    );
                    listView1.Invoke(() =>
                    {
                        item.SubItems[1].Text = "Success";
                        item.ForeColor = Color.Green;
                    });
                }
                catch (Exception e)
                {
                    ErrorCount++;
                    listView1.Invoke(() =>
                    {
                        item.SubItems[1].Text = "Fail";
                        item.SubItems[1].Tag = e;
                        item.ForeColor = Color.Red;
                    });
                    RunningResult = false;
                }
                value += test.TaskCount;
            }
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
            => backgroundWorker1.RunWorkerAsync();
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Invoke(()=>runToolStripMenuItem.Enabled = false);
            Run();
            Invoke(() => runToolStripMenuItem.Enabled = true);
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
            => Run();
        private void TestForm_SizeChanged(object sender, EventArgs e)=>TestName.Width= Result.Width=Progress.Width=Info.Width = Width / 4;
        private void TestForm_Load(object sender, EventArgs e)
        {
            TestName.Width = Result.Width = Progress.Width = Info.Width =listView1.Width / 4;
            splitContainer1.SplitterDistance = splitContainer1.Width;
        }
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
        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            startToolStripMenuItem.Visible = false;
            loadAllToolStripMenuItem.Visible = false;
            Type ITest = typeof(ITest);
            Text = Environment.CurrentDirectory;
            progressBar1.Value = 0;
            SubTaskCount = 0;
            foreach (FileInfo fi in new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles("*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(fi.FullName);
                    foreach (Type t in assembly.GetTypes())
                        if (t.BaseType == ITest)
                        {
                            ITest test = t.GetConstructor(Array.Empty<Type>()).Invoke(null) as ITest;
                            listView1.Items.Add(new ListViewItem(test.TestName)
                            {
                                SubItems = { "Prepared", $"0/{test.TaskCount}", "" },
                                Tag = test
                            });
                            SubTaskCount += test.TaskCount;
                        }
                }
                catch (Exception) { }
            }
            Run();
            if (RunningResult) Application.Exit();
        }
        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                if (!item.Selected)
                    item.Remove();
            Run();
        }
        public bool TestBoxOn = false;
        private void textBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(TestBoxOn)
            {
                splitContainer1.SplitterDistance = splitContainer1.Width;
                TestName.Width = Result.Width = Progress.Width = Info.Width = listView1.Width / 4;
                TestBoxOn = false;
            }
            else
            {
                splitContainer1.SplitterDistance = splitContainer1.Width / 2;
                TestName.Width = Result.Width = Progress.Width = Info.Width = listView1.Width / 4;
                TestBoxOn = true;
            }
        }
    }
}
