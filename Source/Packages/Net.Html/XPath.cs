using Automata;
using Net.Html.Properties;
using System.Collections.Generic;
using System.IO;
namespace Net.Html
{
    public class XPath
    {
        public List<IXPathMode> Modes;
        public static readonly AutomataInstance Instance;
        static XPath()
        {
            using StreamReader streamReader = new(new MemoryStream(Resources.XPath));
            Instance = AKC.ReadFrom(streamReader.ReadToEnd());
        }
        public static XPath CreateFrom(string text)
        {
            XPathHost xPathHost = new();
            Instance.Run(xPathHost, text, 0, text.Length);
            return xPathHost.XPath;
        }
        public XPath(params IXPathMode[] modes)
            => Modes = new List<IXPathMode>(modes);
        public XPath(List<IXPathMode> modes)
            => Modes = modes;
        public IEnumerable<object> Search(params HtmlNode[] node)
        {
            IEnumerable<object> enumerable = node;
            foreach (IXPathMode mode in Modes)
                enumerable = mode.Search(enumerable);
            return enumerable;
        }
    }
}
