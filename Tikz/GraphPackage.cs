using Net.Xml;
using System.Drawing;

namespace Tikz
{
    public class GraphPackage:ITikzPackage
    {
        public GraphPackage()
        {

        }
        public static Bitmap Triangle(XmlNode node, TikzCommandManager _)
        {
            Point[] ps = new Point[node.Nodes.Length];
            for (int i = 0; i < ps.Length; i++)
                ps[i] = new Point(
                    int.Parse(node.Nodes[i].Info["x"]),
                    int.Parse(node.Nodes[i].Info["y"]));
            Bitmap bm = new(int.Parse(node.Info["width"]), int.Parse(node.Info["height"]), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.FillPolygon(new SolidBrush(Color.FromName(node.Info["color"])), ps);
            return bm;
        }
    }
}
