using System.Collections.Generic;
using Collection;

namespace Net.Html
{
	public sealed class XPathMode_GetAttribute
	{
		public string Key;
		public XPathMode_GetAttribute(string key)
			=>Key = key;
		public IEnumerable<object> Search(IEnumerable<object> nodes)
		{
			foreach (HtmlNode node in nodes)
				yield return new HtmlNode(node)
				{
					Text = node.Info[Key, 0],
					Name = "Info",
					Parent = node,
					Nodes = new Collection.List<HtmlNode>(),
					Info = new TrieTree<string>()
				};
		}
	}
}
