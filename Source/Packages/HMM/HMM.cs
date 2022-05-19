using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMM
{
    /// <summary>
    /// 3-Tuple including Transition Possibility, Emission Possibility and Initial Possibility
    /// </summary>
    public class HMM
    {
        public int StateSize;
        public int OutputSize;
        public TrainsitionPossibility A;
        public EmissionPossibility B;
        public StatePossibility Pi;
        public HMM(int statesize,int outputsize)
        {
            StateSize = statesize;
            OutputSize= outputsize;
            A = new(statesize);
            B=new(statesize,outputsize);
            Pi = new(statesize);
        }
    }
}
