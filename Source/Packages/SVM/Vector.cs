namespace SVM
{
    public class Vector
    {
        public double[] Values;
        public double this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }
        public int Dimension=>Values.Length;
        public Vector(params double[] values)=>Values = values;
        public Vector(int length)=>Values = new double[length];
        public static double operator*(Vector a,Vector b)
        {
            double r = 0;
            for(int i=0;i<a.Dimension; i++)
                r+=a[i]*b[i];
            return r;
        }
        public static Vector operator+(Vector a,Vector b)
        {
            double[] r=new double[a.Dimension];
            for(int i=0; i<a.Dimension; i++)
                r[i]=a[i]+b[i];
            return new(r);
        }
        public static Vector operator-(Vector a, Vector b)
        {
            double[] r = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
                r[i] = a[i] - b[i];
            return new(r);
        }
        public static Vector operator*(Vector a, double b)
        {
            double[] r = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
                r[i] = a[i]*b;
            return new(r);
        }
        public static Vector operator *(double a, Vector b)
        {
            double[] r = new double[b.Dimension];
            for (int i = 0; i < b.Dimension; i++)
                r[i] = a*b[i];
            return new(r);
        }
        public double Norm2()
        {
            double r = 0;
            foreach (double value in Values)
                r += value * value;
            return r;
        }
        public override string ToString()
            => $"({string.Join(",", Values)})";
    }
}
