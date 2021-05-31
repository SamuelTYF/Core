using System.Collections.Generic;

namespace Net.Html
{
	public sealed class XPathMode_Attribute : XPathMode
	{
		public string Key;
		public string Value;
		public XPathMode_Attribute(string key, string value)
		{
			Key = key;
			Value = value;
		}
		public IEnumerable<object> Search(IEnumerable<object> nodes)
		{
			foreach (HtmlNode node in nodes)
				if (node.Info[Key, 0] == Value)
					yield return node;
		}
	}
}
