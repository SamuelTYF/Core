using Collection;
namespace Parser
{
    public class ParsedObject
    {
        public string Name;
        public List<ParsedObject> Objects;
        public object Value;
        public bool Select;
        public ParsedObject(IParserTreeNode node)
        {
            Name = node.Name;
            Select =node.Main&&node.Select;
            Objects = new();
        }
        public string Print(string prefix = "") 
        {
            string s=prefix + Name + "\n";
            foreach (ParsedObject obj in Objects)
                s += obj.Print(prefix + "\t");
            if (Objects.Length == 0) s += $"{prefix}\t{Value}\n";
            return s;
        }
        public override string ToString() => Print();
    }
}
