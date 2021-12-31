namespace Operations.LinearProgram.Simplex
{
    /// <summary>
    /// Simplex Standard Form
    /// </summary>
    public class SSF
    {
        public int Row;
        public int XCount;
        public int Column;
        public double[,] A;
        public double[] B;
        public double[] C;
        public int[] Xb;
        public double[] Theta;
        public double[] Sigma;
        public int MC;
        public int MR;
        public SSF(int xcount, int row,params double[] c)
        {
            Row = row;
            XCount = xcount;
            Column = row + xcount;
            A = new double[row, Column];
            B = new double[row];
            C = new double[Column];
            Xb = new int[row];
            for (int i = 0; i < XCount; i++)
                C[i] = c[i];
            for (int i = 0; i < row; i++)
                Xb[i] = xcount + i;
            MC = MR = 0;
            Theta = new double[row];
            Sigma = new double[Column];
        }
        public void RegisterRule(int row, params double[] values)
        {
            for (int i = 0; i < XCount; i++)
                A[row, i] = values[i];
            A[row, XCount + row] = 1;
            B[row] = values[XCount];
        }
        public void RegisterRule(int row, double[] a,double b)
        {
            for (int i = 0; i < a.Length; i++)
                A[row, i] = a[i];
            A[row, XCount + row] = 1;
            B[row] = b;
        }
        public int ComputeMC()
        {
            double c = C[Xb[MR]];
            double max = 0;
            int index = -1;
            for (int i = 0; i < Column; i++)
                Sigma[i] = C[i] - A[MR, i] * c;
            for (int i = 0; i < XCount; i++)
                Sigma[Xb[i]] = 0;
            for(int i=0;i<Column;i++)
                if (Sigma[i] > max)
                {
                    max = Sigma[i];
                    index = i;
                }
            return index;
        }
        public int ComputeMR()
        {
            double min = double.PositiveInfinity;
            int index = -1;
            for(int i=0;i<Row;i++)
            {
                if (A[i, MC] <= 0) Theta[i] = double.NegativeInfinity;
                else
                {
                    Theta[i] = B[i] / A[i, MC];
                    if(Theta[i]<min)
                    {
                        min = Theta[i];
                        index = i;
                    }
                }
            }
            return index;
        }
        public void ComputeA()
        {
            double a = A[MR, MC];
            for (int i = 0; i < Column; i++)
                A[MR, i] /= a;
            B[MR] /= a;
            for (int i = 0; i < Row; i++)
                if (i != MR)
                {
                    double ap = A[i, MC];
                    for (int j = 0; j < Column; j++)
                        A[i, j] -= A[MR, j] * ap;
                    B[i] -= B[MR] * ap;
                }
        }
        public bool Run()
        {
            if ((MC = ComputeMC()) < 0) return true;
            if ((MR = ComputeMR()) < 0) return true;
            Xb[MR] = MC;
            ComputeA();
            return false;
        }
        public override string ToString()
        {
            string s = $"\t\t{string.Join("\t", C)}\n";
            for (int i = 0; i < Row; i++)
            {
                s += $"{C[Xb[i]]}\t{Xb[i]}\t";
                for (int j = 0; j < Column; j++)
                    s += $"{A[i, j]}\t";
                s += $"{B[i]}\t{Theta[i]}\n";
            }
            s += $"\t\t{string.Join("\t", Sigma)}\n";
            return s;
        }
        public double[] GetResult()
        {
            while (!Run()) ;
            double[] r = new double[XCount];
            for (int i = 0; i < Row; i++)
                if(Xb[i]< XCount)
                    r[Xb[i]] = B[i];
            return r;
        }
    }
}
