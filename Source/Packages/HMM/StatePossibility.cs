namespace HMM
{
    public class StatePossibility
    {
        public int StateSize;
        public double[] Possibilities;
        public double this[int index]
        {
            get => Possibilities[index];
            set => Possibilities[index] = value;
        }
        public StatePossibility(int statesize)
        {
            StateSize = statesize;
            Possibilities = new double[statesize];
        }
        public double Normalized()
        {
            double r = 0;
            for (int state = 0; state < StateSize; state++)
                r += Possibilities[state];
            for (int state = 0; state < StateSize; state++)
                Possibilities[state] /= r;
            return r;
        }
    }
}
