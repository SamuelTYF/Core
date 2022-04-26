namespace SVM
{
    public class SMO
    {
        public SVM SVM;
        public Vector Alpha;
        public Vector Error;
        public double C;
        public double Threshold;
        public SMO(SVM svm,double c,double threshold)
        {
            SVM = svm;
            Alpha = new(svm.N);
            Error = new(svm.N);
            C = c;
            Threshold = threshold;
        }
        public Vector GetFullOmega()
        {
            Vector omega = new(SVM.Dimension);
            for (int i = 0; i < SVM.N; i++)
                omega += Alpha[i] * SVM.Y[i] * SVM.X[i];
            return omega;
        }
        public double F(Vector x) => SVM.Omega * x + SVM.B;
        public double E(int i) => F(SVM.X[i]) - SVM.Y[i];
        public void ComputeError()
        {
            for (int i = 0; i < SVM.N; i++)
                Error[i] = E(i);
        }
        public double Bound(double inf, double alpha, double sup) 
            => alpha > sup ? sup : (alpha < inf ? inf : alpha);
        public double GetB()
        {
            double sum = 0;
            int count = 0;
            for (int i = 0; i < SVM.N; i++)
                if (Alpha[i] > 0)
                {
                    sum += SVM.Y[i] - SVM.Omega * SVM.X[i];
                    count++;
                }
            return sum / count;
        }
        public void Adjust(int i, int j)
        {
            if (i == j) throw new Exception();
            double alphai = Alpha[i];
            double alphaj = Alpha[j];
            Vector xi = SVM.X[i];
            Vector xj = SVM.X[j];
            double yi = SVM.Y[i];
            double yj = SVM.Y[j];
            double inf = yi == yj ?
                Math.Max(0, alphai + alphaj - C) :
                Math.Max(0, alphai - alphaj);
            double sup = yi == yj ?
                Math.Min(C, alphai + alphaj) :
                Math.Min(C, alphai - alphaj + C);
            if (inf == sup)
            {
                Console.WriteLine("inf=sup");
                return;
            }
            double ei = E(i);
            double ej = E(j);
            if(!((yi*ei<-Threshold&&alphai<C)||(yi*ei>Threshold&&alphai>0)))
            {
                Console.WriteLine("Error Less Then Threshold");
                return;
            }
            double deltaalphai = yi * (ej - ei) / (xi - xj).Norm2();
            if (Math.Abs(deltaalphai) < Threshold)
            {
                Console.WriteLine("Delta Alpha Less Than Threshold");
                return;
            }
            double newalphai = Bound(inf, alphai + deltaalphai, sup);
            double newalphaj = Bound(0,alphaj + (alphai - newalphai) * yi * yj,C);
            if (newalphai < 0 || newalphai > C) throw new Exception();
            if (newalphaj < 0 || newalphaj > C) throw new Exception();
            Console.WriteLine($"Alpha\t{newalphai}\t{newalphaj}");
            double bi = SVM.B - ei - yi * (newalphai - alphai) * xi * xi - yj * (newalphaj - alphaj) * xi * xj;
            double bj = SVM.B - ej - yj * (newalphaj - alphaj) * xj * xj - yi * (newalphai - alphai) * xi * xj;
            Console.WriteLine($"B\t{bi}\t{bj}");
            SVM.Omega += (newalphai - alphai) * yi * xi + (newalphaj - alphaj) * yj * xj;
            //if (0 < newalphai && newalphai < C) SVM.B = bi;
            //else if(0<newalphaj&&newalphaj<C)SVM.B = bj;
            //else SVM.B = (bi + bj) / 2;
            Alpha[i] = newalphai;
            Alpha[j] = newalphaj;
            SVM.B = GetB();
            Console.WriteLine($"B\t{SVM.B}");
            Console.WriteLine($"Omega\t{SVM.Omega}");
            Console.WriteLine($"Omega\t{GetFullOmega()}");
        }
        public void Run(int time)
        {
            Random r = new(DateTime.Now.Millisecond);
            while(time-->0)
            {
                for(int i=0;i<SVM.N;i++)
                {
                    int j;
                    do j = r.Next(SVM.N); while (i == j);
                    Adjust(i, j);
                }
            }
        }
        public void GetError()
        {
            Vector omega = new(SVM.N);
            for (int i = 0; i < SVM.N; i++)
                omega[i] = E(i);
            Console.WriteLine(omega);
        }
    }
}
