namespace HMM
{
    public class EmissionPossibility
    {
        public int StateSize;
        public int OutputSize;
        public double[,] Possibilities;
        public double this[int state,int output]
        {
            get => Possibilities[state, output];
            set => Possibilities[state, output] = value;
        }
        public EmissionPossibility(int statesize,int outputsize)
        {
            StateSize = statesize;
            OutputSize = outputsize;
            Possibilities = new double[statesize, outputsize];
        }
    }
}
