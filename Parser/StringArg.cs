using System.Text;
namespace Parser
{
    public class StringArg : IStringArg
    {
        public string Value;
        public StringArg(string value)
        {
            Value = value;
            State = new(0, value[0], value.Length>1);
        }
        public override void MoveToNext()
        {
            int index = Index + 1;
            State = index < Value.Length ? (new(index, Value[index], true)) : (new(index,'\0', false));
        }
        public override void Dispose() => System.GC.SuppressFinalize(this);
        public override string Get(int index, int length) => Value.Substring(index, length);
    }
}
