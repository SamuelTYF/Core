using System.Collections.Generic;
namespace Net.Html
{
    public sealed class XPathMode_Index : IXPathMode
    {
        public int Index;
        public XPathMode_Index(int index) => Index = index;
        public IEnumerable<object> Search(IEnumerable<object> nodes)
        {
            foreach (HtmlNode node in nodes)
                if (Index < 0)
                {
                    int num = node.Nodes.Length + Index;
                    if (num >= 0 && node.Nodes.Length > num)
                        yield return node.Nodes[num];
                }
                else if (node.Nodes.Length > Index)
                    yield return node.Nodes[Index];
        }
    }
}
