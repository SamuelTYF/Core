using Net.Xml;
using System.Drawing;

namespace Tikz
{
    public class FormulaPackage:ITikzPackage
    {
        public static readonly Brush Black = new SolidBrush(Color.Black);
        public static readonly PointF ZeroPoint = new(0, 0);
        public static readonly Font Times = new(
            new FontFamily("Times New Roman"),
            20);
        public static Bitmap Text(XmlNode node, TikzCommandManager manager)
        {
            string s = node.Nodes[0].Text;
            Size size = manager._G.MeasureString(s, Times).ToSize();
            Bitmap bm = new(size.Width+1, size.Height+1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics.FromImage(bm).DrawString(s, Times, Black, ZeroPoint);
            return bm;
        }
        public static Bitmap Combine(XmlNode node, TikzCommandManager manager)
        {
            Bitmap[] bms = new Bitmap[node.Nodes.Length];
            double heightmax = 0;
            for (int i = 0; i < node.Nodes.Length; i++)
            {
                bms[i] = manager.GetBitmap(node.Nodes[i]);
                if (bms[i].Height > heightmax) heightmax = bms[i].Height;
            }
            double width = 0;
            foreach (Bitmap bm in bms)
                width += bm.Width * heightmax / bm.Height;
            Bitmap r = new((int)width+1, (int)heightmax+1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(r);
            double w = 0;
            foreach(Bitmap bm in bms)
            {
                double bmw= bm.Width * heightmax / bm.Height;
                g.DrawImage(bm, (float)w, 0, (float)bmw, (float)heightmax);
                w += bmw;
            }
            return r;
        }
    }
}
