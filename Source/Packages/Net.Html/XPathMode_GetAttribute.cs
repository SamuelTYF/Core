using Collection;
namespace Net.Html
{
    public sealed class XPathMode_GetAttribute
    {
        public string Key;
        public XPathMode_GetAttribute(string key)
            => Key = key;
        public System.Collections.Generic.IEnumerable<object> Search(System.Collections.Generic.IEnumerable<object> nodes)
        {
            foreach (HtmlNode node in nodes)
                yield return new HtmlNode(node)
                {
                    Text = node.Info[Key, 0],
                    Name = "Info",
                    Parent = node,
                    Nodes = new List<HtmlNode>(),
                    Info = new TrieTree<string>()
                };
        }
    }
}
