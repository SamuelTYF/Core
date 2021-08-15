using NumericalAnalysis;
using System.Drawing;
namespace ColorFunction
{
    public class RGBFunction
    {
        public Polynomial R;
        public Polynomial G;
        public Polynomial B;
        public RGB this[double x]=>new(R[x],G[x],B[x]);
        public RGBFunction(Polynomial r,Polynomial g,Polynomial b)
        {
            R = r;
            G = g;
            B = b;
        }
        public Bitmap GetBitmap(int width,int height)
        {
            Bitmap bm = new(width, height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            for(int i=0;i<width;i++)
                g.FillRectangle(new SolidBrush(this[1.0 * i / width]), i, 0, 10, height);
            return bm;
        }
    }
}
