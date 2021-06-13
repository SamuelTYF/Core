using Net.Xml;
using Tikz;
using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace TikzForm
{
    public partial class MainForm : Form
    {
        public TikzCommandManager CommandManager;
        public MainForm()
        {
            InitializeComponent();
            CommandManager = new TikzCommandManager();
            CommandManager.Register(new FormulaPackage());
            CommandManager.Register(new GraphPackage());
            CommandManager.Register(new ChartPackage());
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using MemoryStream ms = new(Encoding.UTF8.GetBytes(XmlBox.Text.Replace("\n","")));
                XmlReader xml = new(new XmlStream(ms));
                xml.Read();
                TikzPicture.Image = CommandManager.GetBitmap(xml.MainNode.Nodes[0]);
            }
            catch (Exception)
            {

            }
        }
    }
}
