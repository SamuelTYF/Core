using System;
using System.Collections.Generic;

namespace Net.Xml
{
    public class XmlReader
    {
        public XmlStream Source;
        public byte[] Temp;
        public int P;
        public XmlNode MainNode;
        public XmlReader(XmlStream stream)
        {
            MainNode = new XmlNode(null)
            {
                Name = "xml"
            };
            Source = stream;
        }
        public void Read()
        {
            char c = Source.ReadNext();
            while(Source.CanRead)
            {
                if(c=='<')
                {
                    c = Source.ReadNext();
                    if (c == '?') MainNode.Nodes.Add(ReadXmlInfo());
                    else break;
                }
                c = Source.ReadNext();
            }
            Source.Back('<', c);
            MainNode.Nodes.AddRange(ReadNodes(MainNode));
        }
        public XmlNode ReadXmlInfo()
        {
            XmlNode info = new(MainNode);
            info.Name = "html";
            string s = "<?";
            char c = Source.ReadNext();
            a:while (c != '?') { s += c; c = Source.ReadNext(); }
            s += '?';
            c = Source.ReadNext();
            if (c == '>')
            {
                s += '>';
                info.Text = s;
                info.Info = InfoAnalysis.Run(s);
            }
            else
            {
                s += c;
                goto a;
            }
            return info;
        }
        public IEnumerable<XmlNode> ReadNodes(XmlNode parent)
        {
            string text = "";
            char temp = Source.ReadNext();
            while (Source.CanRead)
            {
                if (temp == '<')
                {
                    text += temp;
                    temp = Source.ReadNext();
                    if (temp == '/')
                    {
                        string name = "";
                        temp = Source.ReadNext();
                        while (temp != '>')
                        {
                            name += temp;
                            temp = Source.ReadNext();
                        }
                        if (name == parent.Name)
                        {
                            parent.Text += $"</{name}>";
                            parent.Parent.Text += parent.Text;
                            yield break;
                        }
                        else throw new Exception();
                    }
                    else
                    {
                        string name = "";
                        while (temp is not (' ' or '>' or '/'))
                        {
                            name += temp;
                            temp = Source.ReadNext();
                        }
                        text += name;
                        if (temp == ' ') while (temp is not ('>' or '/'))
                            {
                                text += temp;
                                temp = Source.ReadNext();
                            }
                        if (temp == '/')
                        {
                            if (Source.ReadNext() != '>') throw new Exception();
                            text += "/>";
                            XmlNode node = new(parent);
                            node.Name = name;
                            node.Text = text;
                            node.Info = InfoAnalysis.Run(text);
                            text = "";
                            yield return node;
                        }
                        else
                        {
                            text += '>';
                            XmlNode node = new(parent);
                            node.Name = name;
                            node.Text = text;
                            node.Info = InfoAnalysis.Run(text);
                            node.Nodes.AddRange(ReadNodes(node));
                            text = "";
                            yield return node;
                        }
                    }
                }
                else if (temp == '\0') yield break;
                else if (temp != ' ')
                {
                    while (temp != '<')
                    {
                        text += temp;
                        temp = Source.ReadNext();
                    }
                    Source.Back('<');
                    XmlNode node = new(parent);
                    node.Text = text;
                    node.Name = "html.Text";
                    parent.Text += text;
                    yield return node;
                }
                temp = Source.ReadNext();
            }
            yield break;
        }
    }
}
