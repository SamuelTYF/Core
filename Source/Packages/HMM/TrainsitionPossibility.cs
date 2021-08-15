using System;

namespace HMM
{
    /// <summary>
    /// Aij represents the trainsition possibility of state i to state j
    /// </summary>
    public class TrainsitionPossibility
    {
        public int StateSize;
        public double[,] Possibilities;
        public double this[int i,int j]
        {
            get => Possibilities[i, j];
            set => Possibilities[i, j] = value;
        }
        public TrainsitionPossibility(int statesize)
        {
            StateSize = statesize;
            Possibilities = new double[statesize, statesize];
        }
    }
}
