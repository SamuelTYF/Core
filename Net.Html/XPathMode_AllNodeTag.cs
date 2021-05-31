using System.Collections.Generic;
namespace Net.Html
{
    public sealed class XPathMode_AllNodeTag : IXPathMode
    {
        public string Tag;
        public XPathMode_AllNodeTag(string tag) => Tag = tag;
        public IEnumerable<object> Search(IEnumerable<object> nodes)
        {
            foreach (HtmlNode node in nodes)
            {
                foreach (HtmlNode node2 in node.Nodes)
                    if (node2.Name == Tag)
                        yield return node2;
                foreach (HtmlNode item in Search(node.Nodes))
                    yield return item;
            }
        }
    }
}
