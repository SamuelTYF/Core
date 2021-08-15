using Automata;
using Collection;
using System.Text;
namespace Net.Html
{
    public class XPathHost : AutomataHost
    {
        public XPath XPath;
        public List<byte> TempBytes;
        public string Key;
        public TrieTree<XPathFunction> XPathFunctions;
        public XPathHost()
        {
            XPathFunctions = new TrieTree<XPathFunction>();
            XPathFunctions["text", 0] = (HtmlNode node) => node.Text;
            XPathFunctions["GetInnerText", 0] = (HtmlNode node) => node.GetInnerText();
            XPathFunctions["GetOuterText", 0] = (HtmlNode node) => node.GetOuterText();
            XPathFunctions["GetHref", 0] = (HtmlNode node) => node.Info["href", 0];
            XPathFunctions["GetValue", 0] = (HtmlNode node) => node.Info["value", 0];
        }
        public void Init()
        {
            XPath = new XPath();
            TempBytes = new List<byte>();
        }
        public void Push() => TempBytes.Add((byte)Input);
        public void AddIndex()
        {
            XPath.Modes.Add(new XPathMode_Index(int.Parse(Encoding.UTF8.GetString(TempBytes.ToArray()))));
            TempBytes.Clear();
        }
        public void AddNodeTag()
        {
            XPath.Modes.Add(new XPathMode_NodeTag(Encoding.UTF8.GetString(TempBytes.ToArray())));
            TempBytes.Clear();
        }
        public void AddAllNodeTag()
        {
            XPath.Modes.Add(new XPathMode_AllNodeTag(Encoding.UTF8.GetString(TempBytes.ToArray())));
            TempBytes.Clear();
        }
        public void AddKey()
        {
            Key = Encoding.UTF8.GetString(TempBytes.ToArray());
            TempBytes.Clear();
        }
        public void AddAttribute()
        {
            XPath.Modes.Add(new XPathMode_Attribute(Key, Encoding.UTF8.GetString(TempBytes.ToArray())));
            TempBytes.Clear();
        }
        public void AddGetAttribute()
        {
            XPath.Modes.Add(new XPathMode_NodeTag(Encoding.UTF8.GetString(TempBytes.ToArray())));
            TempBytes.Clear();
        }
        public void AddFunction()
        {
            XPath.Modes.Add(new XPathMode_Function(XPathFunctions[Encoding.UTF8.GetString(TempBytes.ToArray()), 0]));
            TempBytes.Clear();
        }
        public override HashTable<Function> MarkFunctions()
        {
            HashTable<Function> hashTable = new();
            hashTable.Register(Throw, "Throw".GetHashCode());
            hashTable.Register(Init, "Init".GetHashCode());
            hashTable.Register(Push, "Push".GetHashCode());
            hashTable.Register(AddIndex, "AddIndex".GetHashCode());
            hashTable.Register(AddNodeTag, "AddNodeTag".GetHashCode());
            hashTable.Register(AddAllNodeTag, "AddAllNodeTag".GetHashCode());
            hashTable.Register(AddKey, "AddKey".GetHashCode());
            hashTable.Register(AddAttribute, "AddAttribute".GetHashCode());
            hashTable.Register(AddGetAttribute, "AddGetAttribute".GetHashCode());
            hashTable.Register(AddFunction, "AddFunction".GetHashCode());
            return hashTable;
        }
    }
}
