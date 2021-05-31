namespace Automata
{
    public class StringArg : IStringArg
    {
        public char[] str;
        public int index;
        public int end;
        public bool NotOver { get; private set; }
        public StringArg(string s)
        {
            str = s.ToCharArray();
            index = 0;
            end = str.Length;
            NotOver = true;
        }
        public char Top() => str[index];
        public void Pop() => NotOver = ++index != end;
        public char Last() => str[index - 1];
    }
}
