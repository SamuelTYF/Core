using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMM
{
    public class OutputSequence
    {
        public int T;
        public int[] Output;
        public int this[int t]
        {
            get => Output[t];
            set => Output[t] = value;
        }
        public OutputSequence(int t)
        {
            T = t;
            Output = new int[t];
        }
        public OutputSequence(params int[] outputs)
        {
            T=outputs.Length;
            Output = outputs;
        }
    }
}
