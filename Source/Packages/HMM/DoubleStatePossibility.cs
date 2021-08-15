namespace HMM
{
    public class DoubleStatePossibility
    {
        public int StateSize;
        public double[,] Possibilities;
        public double this[int i,int j]
        {
            get => Possibilities[i, j];
            set=> Possibilities[i, j] = value;
        }
        public DoubleStatePossibility(int statesize)
        {
            StateSize = statesize;
            Possibilities=new double[statesize,statesize];
        }
        public void Normalized()
        {
            double r = 0;
            for (int i = 0; i < StateSize; i++)
                for (int j = 0; j < StateSize; j++)
                    r += Possibilities[i, j];
            for (int i = 0; i < StateSize; i++)
                for (int j = 0; j < StateSize; j++)
                    Possibilities[i, j] /= r;
        }
    }
}
