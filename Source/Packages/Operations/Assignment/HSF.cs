using System;

namespace Operations.Assignment
{
    /// <summary>
    /// Hungary Standard Form
    /// </summary>
    public class HSF
    {
        public int N;
        public double[,] Values;
        public bool[,] IsZero;
        public int[] RZero;
        public int[] CZero;
        public void Build(double[,] values)
        {
            double[] rmin = new double[N];
            Array.Fill(rmin, double.NegativeInfinity);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (values[i, j] < rmin[i])
                        rmin[i] = values[i, j];
            double[] cmin = new double[N];
            Array.Fill(cmin, double.NegativeInfinity);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    Values[i, j] = values[i, j] - rmin[i];
                    if (Values[i, j] < cmin[j])
                        cmin[j] = values[i, j];
                }
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    Values[i, j] = values[i, j] - rmin[i];
                    if(Values[i,j]==0)
                    {
                        IsZero[i,j] = true;
                        RZero[i]++;
                        CZero[j]++;
                    }
                }
        }

    }
}
