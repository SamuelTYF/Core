using System.Collections.Generic;
namespace Net.Html
{
    public sealed class XPathMode_Function : IXPathMode
    {
        public XPathFunction Function;
        public XPathMode_Function(XPathFunction function)
            => Function = function;
        public IEnumerable<object> Search(IEnumerable<object> nodes)
        {
            foreach (HtmlNode node in nodes)
                yield return Function(node);
        }
    }
}
