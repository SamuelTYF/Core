using System;

namespace PSO
{
    public class Vector
    {
        public double[] Values;
        public int Length;
        public Vector(double[] values) => Length=(Values = values).Length;
        public Vector(int length)=>Values = new double[Length = length];
        public Vector(int length,Random r)
        {
            Values = new double[Length = length];
            for (int i = 0; i < Length; i++)
                Values[i] = r.NextDouble();
        }
        public static Vector operator *(Vector a, double b)
        {
            double[] r = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                r[i] = a.Values[i] * b;
            return new(r);
        }
        public static Vector operator *(double b, Vector a)
        {
            double[] r = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                r[i] = a.Values[i] * b;
            return new(r);
        }
        public static Vector operator+(Vector a,Vector b)
        {
            if (a.Length != b.Length) throw new ArgumentException();
            double[] r = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                r[i] = a.Values[i] +b.Values[i];
            return new(r);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.Length != b.Length) throw new ArgumentException();
            double[] r = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                r[i] = a.Values[i] - b.Values[i];
            return new(r);
        }
        public override string ToString() => string.Join(",", Values);
    }
}
