using Net.Xml;
using System.IO;
using TestFramework;
namespace Net.Test
{
    public class Xml_Test:ITest
    {
        public Xml_Test()
            :base("Xml_Test",5)
        { 
        }
        public override void Run(UpdateTaskProgress update)
        {
            using FileStream fs = new FileInfo("..\\Net.Xml\\Net.Xml.csproj").OpenRead();
            XmlStream stream = new(fs);
            update(1);
            XmlReader reader = new(stream);
            update(2);
            reader.Read();
            update(3);
            XmlNode node = reader.MainNode.Nodes[0];
            update(4);
            UpdateInfo(node);
            FileInfo fi = new("Net.Xml.xml");
            using StreamWriter sw = new(fi.OpenWrite());
            sw.Write(node.Print());
            sw.Dispose();
            update(5);
            fi.Delete();
        }
    }
}
