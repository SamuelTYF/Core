using System.Drawing;
using Net.Xml;
namespace Tikz
{
    public delegate Bitmap TikzParser(XmlNode node,TikzCommandManager manager);
}
