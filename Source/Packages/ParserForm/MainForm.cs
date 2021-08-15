using Parser;
using System;
using System.Windows.Forms;

namespace ParserForm
{
    public partial class MainForm : Form
    {
        public RichTextBoxStringArg ParserArg;
        public RichTextBoxStringArg SourceArg;
        public MainForm()
        {
            InitializeComponent();
            ParserArg = new(ParserBox);
            SourceArg = new(SourceBox);
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IParser parser = TreeBox.SelectedItem as IParser;
            SourceArg.Update();
            IParseResult result = parser.Parse(SourceArg);
            MessageBox.Show(result.Success.ToString());
        }
        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParserArg.Load(@"D:\Core\Parser\Resources\CSharp.txt");
            SourceArg.Load(@"D:\Core\Parser\FromJson.cs");
        }
        private void analyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ParserBox.Update();
                TreeBox.Items.Clear();
                var tree = GrammarParser.GetTree(GrammarParser.GetParsedObject(ParserArg));
                tree.Foreach((t) =>
                {
                    if (t.Value.Main)
                        TreeBox.Items.Add(t.Value.Install());
                });
                TreeBox.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
