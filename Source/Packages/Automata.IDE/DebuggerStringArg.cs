using System;
using System.Drawing;
using System.Windows.Forms;
namespace Automata.IDE
{
    public class DebuggerStringArg : IStringArg
    {
        public RichTextBox RichTextBox;
        public StringArg StringArg;
        public Func<bool> TextOn;
        public bool NotOver => StringArg.NotOver;
        public DebuggerStringArg(RichTextBox richTextBox, Func<bool> textOn)
        {
            TextOn = textOn;
            RichTextBox = richTextBox;
            StringArg = new StringArg(RichTextBox.Text);
            if (TextOn())
                RichTextBox.Focus();
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Blue;
            richTextBox.Select(0, 0);
        }
        public char Top()
        {
            if (TextOn())
                RichTextBox.Focus();
            RichTextBox.Select(StringArg.index, 1);
            RichTextBox.SelectionColor = Color.Red;
            return StringArg.Top();
        }
        public (char, bool) GetTop()
        {
            if (TextOn())
                RichTextBox.Focus();
            RichTextBox.Select(StringArg.index, 1);
            RichTextBox.SelectionColor = Color.Red;
            return (StringArg.Top(), RichTextBox.SelectionBackColor == Color.Red);
        }
        public void Pop()
        {
            if (TextOn())
                RichTextBox.Focus();
            RichTextBox.Select(StringArg.index, 1);
            RichTextBox.SelectionColor = Color.Black;
            StringArg.Pop();
        }
        public char Last() => StringArg.Last();
    }
}
