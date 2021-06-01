using System.Drawing;
using System.Windows.Forms;
namespace Automata.IDE
{
    public class RichTextStringArg : IStringArg
    {
        public RichTextBox RichTextBox;
        public StringArg StringArg;
        public bool Flush;
        public bool NotOver => StringArg.NotOver;
        public RichTextStringArg(RichTextBox richTextBox, bool flush)
        {
            Flush = flush;
            RichTextBox = richTextBox;
            StringArg = new StringArg(RichTextBox.Text);
            if (Flush)
                RichTextBox.Focus();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black;
            RichTextBox.SelectionBackColor = Color.White;
            richTextBox.Select(0, 0);
        }
        public char Top() => StringArg.Top();
        public void Pop() => StringArg.Pop();
        public void SetColor(int index, int length, Color color)
        {
            if (Flush)
                RichTextBox.Focus();
            RichTextBox.Select(index, length);
            RichTextBox.SelectionColor = color;
        }
        public void SetBackColor((int index, int length) region)
        {
            if (Flush)
                RichTextBox.Focus();
            RichTextBox.SelectionBackColor = Color.White;
            RichTextBox.Select(region.index, region.length);
            RichTextBox.SelectionBackColor = Color.FromArgb(128, 250, 100, 100);
        }
        public char Last() => StringArg.Last();
    }
}
