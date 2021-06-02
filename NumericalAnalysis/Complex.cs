using System;
using Collection.Serialization;
namespace NumericalAnalysis
{
    public class Complex : ISerializable
    {
        public double R;
        public double I;
        public static readonly Complex Zero = new(0.0);
        public static readonly Complex One = new(1.0);
        public Complex(double r, double i = 0.0)
        {
            R = r;
            I = i;
        }
        public Complex(Formatter formatter)
            : this((double)formatter.Read(), (double)formatter.Read())
        {
        }
        public void Write(Formatter formatter)
        {
            formatter.Write(R);
            formatter.Write(I);
        }
        public static Complex operator *(Complex a, Complex b) => new(a.R * b.R - a.I * b.I, a.R * b.I + a.I * b.R);
        public static Complex operator *(Complex a, double b) => new(a.R * b, a.I * b);
        public static Complex operator *(Complex a, int b) => new(a.R * b, a.I * b);
        public static Complex operator *(int b, Complex a) => new(a.R * b, a.I * b);
        public Complex Pow() => this * this;
        public static Complex operator -(Complex a) => new(-a.R, -a.I);
        public Complex Conjugate() => new(R, -I);
        public static Complex operator /(Complex a, double b) => new(a.R / b, a.I / b);
        public static Complex operator /(Complex a, Complex b)
        {
            double r = b.Norm2();
            return new((a.R * b.R + a.I * b.I) / r, (a.I * b.R - a.R * b.I) / r);
        }
        public static Complex operator +(Complex a, Complex b) => new(a.R + b.R, a.I + b.I);
        public static Complex operator +(Complex a, double b) => new(a.R + b, a.I);
        public static Complex operator -(Complex a, Complex b) => new(a.R - b.R, a.I - b.I);
        public static Complex operator ++(Complex a) => new(a.R + 1.0, a.I);
        public double Norm2() => R * R + I * I;
        public double Norm() => Math.Sqrt(R * R + I * I);
        public double Phi() => Math.Atan2(I, R);
        public Complex Sqrt()
        {
            double i = Norm();
            return new(Math.Sqrt((i + R) / 2.0), I.CompareTo(0.0) * Math.Sqrt((i - R) / 2.0));
        }
        public override string ToString() => R == 0.0
                ? $"{I}i"
                : I.CompareTo(0.0) switch
                {
                    0 => R.ToString(),
                    1 => $"{R}+{I}i",
                    -1 => $"{R}{I}i",
                    _ => throw new Exception(),
                };
        public int Sign() => R.CompareTo(0.0);
    }
}