using System;
using Collection;
namespace Net.Html
{
    public class NodeBuilder : IDisposable
    {
        public List<string> Source;
        public int P;
        public NodeBuilder(List<string> source) => Source = source;
        public void Dispose() => GC.SuppressFinalize(this);
        public HtmlNode Search()
        {
            P = -1;
            string text = Source[++P];
            while (text.StartsWith("<!") || text.StartsWith("<html"))
                text = Source[++P];
            P--;
            HtmlNode htmlNode = new(null)
            {
                Info = InfoAnalysis.Run(Source[P]),
                Name = "html",
                Text = Source[P]
            };
            foreach (HtmlNode item in Search(htmlNode, "</html>"))
                htmlNode.Nodes.Add(item);
            return htmlNode;
        }
        internal System.Collections.Generic.IEnumerable<HtmlNode> Search(HtmlNode parent, string ps)
        {
            string text = Source[++P];
            while (text != ps)
            {
                if (text.Length > 1 && text[0] == '<' && text[^1] == '>' && text[1] != '!')
                {
                    if (text[1] == '/') break;
                    string text2 = "";
                    int num = 0;
                    while (text[++num] != ' ' && text[num] != '/' && text[num] != '>')
                        text2 += text[num];
                    HtmlNode htmlNode = new(parent)
                    {
                        Info = InfoAnalysis.Run(text),
                        Name = text2,
                        Text = text
                    }; 
                    string text3 = "</" + text2 + ">";
                    List<HtmlNode> Nodes = new();
                    if (text[^2] == '/')
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
                    HtmlNode htmlNode2 = new(parent)
                    {
                        Text = text,
                        Name = "Text"
                    };
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
