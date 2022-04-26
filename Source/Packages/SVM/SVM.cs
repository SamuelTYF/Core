namespace SVM
{
    public class SVM
    {
        public int Dimension;
        public int N;
        public Vector[] X;
        public Vector Y;
        public Vector Omega;
        public double B;
        public SVM(Vector[] x, Vector y)
        {
            if (x.Length == 0) throw new ArgumentException();
            if(x.Length!=y.Dimension) throw new ArgumentException();
            Dimension = x[0].Dimension;
            N = x.Length;
            for (int i=1;i<x.Length;i++)
                if(x[i].Dimension!=Dimension)throw new ArgumentException();
            X = x;
            Y = y;
            Omega = new(Dimension);
            B = 0;
        }
    }
}