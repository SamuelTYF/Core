using System.Collections.Generic;

namespace Net.Html
{
	public sealed class XPathMode_NodeTag : XPathMode
	{
		public string Tag;
		public XPathMode_NodeTag(string tag)=>Tag = tag;
		public IEnumerable<object> Search(IEnumerable<object> nodes)
		{
			foreach (HtmlNode node in nodes)
				foreach (HtmlNode node2 in node.Nodes)
					if (node2.Name == Tag)
						yield return node2;
		}
	}
}
