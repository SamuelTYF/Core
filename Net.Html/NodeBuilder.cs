using System;
using System.Collections.Generic;

namespace Net.Html
{
	public class NodeBuilder : IDisposable
	{
		public Collection.List<string> Source;
		public int P;
		public NodeBuilder(Collection.List<string> source)=>Source = source;
		public void Dispose()=>Source = null;
		public HtmlNode Search()
		{
			P = -1;
			string text = Source[++P];
			while (text.StartsWith("<!") || text.StartsWith("<html"))
				text = Source[++P];
			P--;
			HtmlNode htmlNode = new HtmlNode(null);
			htmlNode.Info = InfoAnalysis.Run(Source[P]);
			htmlNode.Name = "html";
			htmlNode.Text = Source[P];
			foreach (HtmlNode item in Search(htmlNode, "</html>"))
				htmlNode.Nodes.Add(item);
			return htmlNode;
		}
		internal IEnumerable<HtmlNode> Search(HtmlNode parent, string ps)
		{
			string text = Source[++P];
			while (text != ps)
			{
				if (text.Length > 1 && text[0] == '<' && text[text.Length - 1] == '>' && text[1] != '!')
				{
					if (text[1] == '/')break;
					string text2 = "";
					int num = 0;
					while (text[++num] != ' ' && text[num] != '/' && text[num] != '>')
						text2 += text[num];
					HtmlNode htmlNode = new HtmlNode(parent);
					htmlNode.Info = InfoAnalysis.Run(text);
					htmlNode.Name = text2;
					htmlNode.Text = text;
					string text3 = "</" + text2 + ">";
					Collection.List<HtmlNode> Nodes = new Collection.List<HtmlNode>();
					if (text[text.Length - 2] == '/')
						yield return htmlNode;
					else
					{
						foreach (HtmlNode item in Search(htmlNode, text3))
							Nodes.Add(item);
						if (Source[P][1] != '/')
							throw new Exception();
						if (!(Source[P] == text3))
						{
							yield return htmlNode;
							foreach (HtmlNode item2 in Nodes)
								yield return item2;
							break;
						}
						htmlNode.Nodes = Nodes;
						foreach (HtmlNode item3 in Nodes)
							item3.Parent = htmlNode;
						yield return htmlNode;
					}
				}
				else
				{
					HtmlNode htmlNode2 = new HtmlNode(parent);
					htmlNode2.Text = text;
					htmlNode2.Name = "Text";
					yield return htmlNode2;
				}
				if (P != Source.Length - 1)
				{
					text = Source[++P];
					continue;
				}
				break;
			}
		}
	}
}
