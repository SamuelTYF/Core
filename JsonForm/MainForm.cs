using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Hook;
using Parser;

namespace JsonForm
{
    public partial class MainForm : Form
    {
        public HookManager Manager;
        public int SelectIndex;
        public MainForm()
        {
            InitializeComponent();
            Manager = new();
            Manager.Register((Action<string>)Display);
            Manager.Register((Action<int>)Highlight);
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == 74)
                {
                    m.Result = Manager.Execute(m.LParam);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            base.WndProc(ref m);
        }
        public void Display(string json)
        {
            JsonBox.Text = json;
            SelectIndex = 0;
        }
        public void Highlight(int index)
        {
            JsonBox.Select(SelectIndex, index - SelectIndex);
            JsonBox.SelectionColor = Color.Red;
            JsonBox.Select(SelectIndex=index, 0);
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                using StreamReader sr = new(openFileDialog1.FileName);
                JsonBox.Text = sr.ReadToEnd();
            }
        }
        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParserTree tree = new();
            tree.FromJson(Parser.Properties.Resources.Json);
            IParser Parser_Json = tree.Trees["json"].Install();

        }
    }
}
