using System.Collections.Generic;
namespace Net.Html
{
    public sealed class XPathMode_AllNodeTag : IXPathMode
    {
        public string Tag;
        public XPathMode_AllNodeTag(string tag) => Tag = tag;
        public IEnumerable<object> Search(HtmlNode node)
        {
            if (node.Name == Tag)
                yield return node;
            foreach(HtmlNode result in Search(node.Nodes))
                yield return result;
        }
        public IEnumerable<object> Search(IEnumerable<object> nodes)
        {
            foreach (HtmlNode node in nodes)
                foreach (HtmlNode result in Search(node))
                    yield return result;
        }
    }
}
