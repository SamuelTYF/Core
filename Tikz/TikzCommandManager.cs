using Collection;
using System.Drawing;
using System.Drawing.Imaging;
using Net.Xml;
namespace Tikz
{
    public class TikzCommandManager
    {
        public Graphics _G;
        public TrieTree<TikzParser> Parsers;
        public TikzCommandManager()
        {
            Parsers = new();
            _G = Graphics.FromImage(new Bitmap(100, 100));
        }
        public void Register(ITikzPackage package)
            => package.Parsers.Foreach((key, value) => Parsers[key] = value.Value);
        public Bitmap GetBitmap(XmlNode node)
        {
            TikzParser parser = Parsers[node.Name];
            return parser(node,this);
        }
    }
}
