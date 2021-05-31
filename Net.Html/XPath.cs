using System.Collections.Generic;
using System.IO;
using Automata;
using Net.Html.Properties;

namespace Net.Html
{
	public class XPath
	{
		public Collection.List<XPathMode> Modes;
		public static readonly AutomataInstance Instance;
		static XPath()
		{
			using StreamReader streamReader = new StreamReader(new MemoryStream(Resources.XPath));
			Instance = AKC.ReadFrom(streamReader.ReadToEnd());
		}
		public static XPath CreateFrom(string text)
		{
			XPathHost xPathHost = new XPathHost();
			Instance.Run(xPathHost, text, 0, text.Length);
			return xPathHost.XPath;
		}
		public XPath(params XPathMode[] modes)
			=>Modes = new Collection.List<XPathMode>(modes);
		public XPath(Collection.List<XPathMode> modes)
			=>Modes = modes;
		public IEnumerable<object> Search(params HtmlNode[] node)
		{
			IEnumerable<object> enumerable = node;
			foreach (XPathMode mode in Modes)
				enumerable = mode.Search(enumerable);
			return enumerable;
		}
	}
}
