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
                int h= TikzPicture.Image.Height * TikzPicture.Width / TikzPicture.Image.Width;
                Text = h.ToString();
                TikzPicture.Height = h;
                XmlBox.Top = h;
                XmlBox.Width = Width;
                XmlBox.Height = Height - h;
            }
            catch (Exception)
            {

            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int h = TikzPicture.Image == null?0: TikzPicture.Image.Height * TikzPicture.Width / TikzPicture.Image.Width;
            Text = h.ToString();
            TikzPicture.Height = h;
            XmlBox.Top =menuStrip1.Height+ h;
            XmlBox.Width = Width;
            XmlBox.Height = Height- menuStrip1.Height - h;
        }
    }
}
