using Collection;
using System;
using System.Collections.Generic;
namespace Net.Html
{
    public class HtmlNode
    {
        public string Text;
        public string Name;
        public TrieTree<string> Info;
        public HtmlNode Parent;
        public Collection.List<HtmlNode> Nodes;
        public HtmlNode(HtmlNode parent)
        {
            Parent = parent;
            Info = new TrieTree<string>();
            Nodes = new Collection.List<HtmlNode>();
        }
        public HtmlNode GetNode(params int[] index)
        {
            HtmlNode htmlNode = this;
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i] < 0)
                {
                    int num = index[i] + htmlNode.Nodes.Length;
                    if (num >= 0 && htmlNode.Nodes.Length > num)
                        htmlNode = htmlNode.Nodes[num];
                }
                else
                {
                    if (htmlNode.Nodes.Length <= index[i])
                        return null;
                    htmlNode = htmlNode.Nodes[index[i]];
                }
            }
            return htmlNode;
        }
        public override string ToString() => Text;
        public IEnumerable<HtmlNode> Select(Func<HtmlNode, bool> IsSeleted)
        {
            if (IsSeleted(this))
                yield return this;
            for (int i = 0; i < Nodes.Length; i++)
                foreach (HtmlNode item in Nodes[i].Select(IsSeleted))
                    yield return item;
        }
        public string GetOuterText(string p = "")
        {
            string text = p + Text;
            if (Name == "Text")
                return text;
            for (int i = 0; i < Nodes.Length; i++)
                text = text + "\n" + Nodes[i].GetOuterText(p + " ");
            return text + "\n" + p + "</" + Name + ">";
        }
        public string GetInnerText(string p = "")
        {
            string text = "";
            for (int i = 0; i < Nodes.Length; i++)
                text = text + Nodes[i].GetOuterText(p + " ") + ((i != 0) ? "\n" : "");
            return text;
        }
    }
}
