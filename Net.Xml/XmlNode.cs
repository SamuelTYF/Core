using Collection;
namespace Net.Xml
{
    public class XmlNode
    {
        public string Name;
        public string Text;
        public TrieTree<string> Info;
        public List<XmlNode> Nodes;
        public XmlNode Parent;
        public XmlNode this[int index]
        {
            get => Nodes[index];
            set => Nodes[index] = value;
        }
        public string this[string index]
        {
            get => Info[index];
            set => Info[index] = value;
        }
        public XmlNode(XmlNode parent)
        {
            Parent = parent;
            Nodes = new List<XmlNode>();
            Info = new();
        }
        public override string ToString() => Text;
        public string Print(string prefix = "")
        {
            if (Name.CompareTo("html.Text") == 0) return prefix + Text;
            string s = $"{prefix}<{Name}";
            Info.Foreach((key, value) => s += $" {key}=\"{value.Value}\"");
            if (Nodes.Length == 0) return s + "/>";
            s += ">\n";
            foreach (XmlNode node in Nodes)
                s += node.Print(prefix + " ") + "\n";
            return s + $"{prefix}</{Name}>";
        }
    }
}
