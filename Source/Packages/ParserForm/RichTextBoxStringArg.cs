using Parser;
using System.IO;
using System.Windows.Forms;

namespace ParserForm
{
    public class RichTextBoxStringArg : IStringArg
    {
        public RichTextBox TextBox;
        public string Value => TextBox.Text;
        public RichTextBoxStringArg(RichTextBox text) => TextBox = text;
        public int SelectIndex = 0;
        public override void MoveToNext()
        {
            int index = Index + 1;
            State = index < Value.Length ? (new(index, Value[index], true)) : (new(index,'\0', false));
            TextBox.Select(SelectIndex, index - SelectIndex);
            TextBox.SelectionColor = System.Drawing.Color.Red;
            SelectIndex = index;
        }
        public override void Dispose() => System.GC.SuppressFinalize(this);
        public override string Get(int index, int length) => Value.Substring(index, length);
        public void Load(string file)
        {
            using StreamReader sr = new(file);
            TextBox.Text = sr.ReadToEnd();
            Update();
        }
        public void Update()
        {
            TextBox.DeselectAll();
            State = new(0, Value[0], Value.Length > 0);
            SelectIndex = 0;
        }
    }
}
