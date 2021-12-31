using System;

namespace NumericalAnalysis.Convolve
{
    public class Vector
    {
        public double[] Values;
        public int Length => Values.Length;
        public Vector(params double[] values) => Values = values;
        public static Vector operator*(Vector a,Vector b)
        {
            if(a.Length!=b.Length)throw new ArgumentException(nameof(a),nameof(b));
            double[] result = new double[a.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = a.Values[i] * b.Values[i];
            return new(result);
        }
        public static Vector operator/(Vector a, Vector b)
        {
            if (a.Length != b.Length) throw new ArgumentException(nameof(a), nameof(b));
            double[] result = new double[a.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = a.Values[i] / b.Values[i];
            return new(result);
        }
        public static Vector operator~(Vector a)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                result[i] = a.Values[a.Length - 1 - i];
            return new(result);
        }
        public Vector SubVector(int offset,int length)
        {
            double[] result = new double[length];
            Array.Copy(Values, offset, result, 0, length);
            return new(result);
        }
        public Vector Convolve(Vector kernel)
        {
            double[] result = new double[Length + kernel.Length - 1];
            for (int i = 0; i < kernel.Length; i++)
                for (int j = 0; j < Length; j++)
                    result[kernel.Length - 1 - i + j] += Values[j] * kernel.Values[i];
            return new(result);
        }
        public Vector Convolve(Vector kernel, int offset)
            => Convolve(kernel).SubVector(offset, Length);
        public void Scale()
        {
            double sum = 0;
            for (int i = 0; i < Length; i++)
                sum += Values[i];
            for (int i = 0; i < Length; i++)
                Values[i] /= sum;
        }
        public override string ToString() => string.Join(",", Values);
    }
}
