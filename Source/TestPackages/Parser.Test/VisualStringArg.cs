using Hook;
using System.Text;
using Parser;
namespace Parser_Test
{
    public class VisualStringArg : IStringArg
    {
        public string Value;
        public Manager Manager;
        public VisualStringArg(string value)
        {
            Value = value;
            State = new(0, value[0], value.Length>0);
            Manager = new("JsonForm.exe");
            Manager.Start();
            Manager.Execute("Display",value);
        }
        public override void MoveToNext()
        {
            int index = Index + 1;
            State = index < Value.Length ? (new(index, Value[index], true)) : (new(index,'\0', false));
            Manager.Execute("Highlight", index);
        }
        public override void Dispose() => Manager.Dispose();
        public override string Get(int index, int length) => Value.Substring(index, length);
    }
}
