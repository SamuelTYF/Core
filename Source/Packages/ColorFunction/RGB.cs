using System;
using System.Drawing;
namespace ColorFunction
{
    public class RGB : IComparable<RGB>
    {
        public readonly double R;
        public readonly double G;
        public readonly double B;
        public RGB(double r, double g, double b)
        {
            R = r < 0 ? 0 : r > 1 ? 1 : r;
            G = g < 0 ? 0 : g > 1 ? 1 : g;
            B = b < 0 ? 0 : b > 1 ? 1 : b;
        }
        public int CompareTo(RGB other)
        {
            int r = R.CompareTo(other.R);
            if (r == 0)
            {
                r = G.CompareTo(other.G);
                if (r == 0) r = B.CompareTo(other.B);
            }
            return r;
        }
        public static implicit operator RGB(Color c)=>new(c.R/255.0,c.G/255.0,c.B/255.0);
        public static implicit operator Color(RGB rgb)=>Color.FromArgb((int)(rgb.R*255), (int)(rgb.G * 255), (int)(rgb.B * 255));
    }
}
