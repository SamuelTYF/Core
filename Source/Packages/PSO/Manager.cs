using System;
using System.Collections.Generic;
using System.Drawing;

namespace PSO
{
    public class Manager
    {
        public Assess Assess;
        public List<Point> Points;
        public double Rate;
        public double GRate;
        public double PRate;
        public double BestS;
        public Vector BestP;
        public Manager(Assess assess,double rate,double grate,double prate)
        {
            Assess = assess;
            Points = new();
            Rate = rate;
            GRate = grate;
            PRate = prate;
            BestS = double.PositiveInfinity;
        }
        public void RegisterPoint(params double[] ps)=>Points.Add(new(new(ps)));
        public void Update()
        {
            Random r = new(DateTime.Now.Millisecond);
            foreach(Point p in Points)
            {
                p.Update(Assess);
                if(p.S<BestS)
                {
                    BestS = p.S;
                    BestP = p.P;
                }
            }
            foreach (Point p in Points)
                p.Move(r, Rate, GRate, PRate, BestP);
        }
        public Bitmap Print(int xmin,int xmax,int ymin,int ymax,double xs,double ys)
        {
            int X = (int)((xmax - xmin) * xs);
            int Y = (int)((ymax - ymin) * ys);
            Bitmap bm = new(X, Y);
            Graphics g = Graphics.FromImage(bm);
            Brush red = new SolidBrush(Color.Red);
            
            foreach(Point p in Points)
            {
                PointF[] pf = Array.ConvertAll(p.PTraces.ToArray(),
                    (v) => new PointF((float)((v.Values[0] - xmin) * xs), (float)((v.Values[1] - ymin) * ys)));
                for (int i = 0; i < pf.Length - 1; i++)
                    g.DrawLine(new(Color.FromArgb(255*(i+1)/pf.Length,Color.Black), 1), pf[i], pf[i + 1]);
                g.FillEllipse(red, pf[^1].X-10, pf[^1].Y-10, 20, 20);
            }
            return bm;
        }
        public Bitmap Trace(int xmin, int xmax, int ymin, int ymax, double xs, double ys)
        {
            int X = (int)((xmax - xmin) * xs);
            int Y = (int)((ymax - ymin) * ys);
            Bitmap bm = new(X, Y);
            Graphics g = Graphics.FromImage(bm);
            Brush red = new SolidBrush(Color.Red);
            Point p = Points[0];
            {
                PointF[] pf = Array.ConvertAll(p.PTraces.ToArray(),
                    (v) => new PointF((float)((v.Values[0] - xmin) * xs), (float)((v.Values[1] - ymin) * ys)));
                for (int i = 0; i < pf.Length - 1; i++)
                    g.DrawLine(new(Color.FromArgb(255 * (i + 1) / pf.Length, Color.Black), 1), pf[i], pf[i + 1]);
                g.FillEllipse(red, pf[^1].X - 10, pf[^1].Y - 10, 20, 20);
            }
            return bm;
        }
    }
}
