using System;

namespace SAA
{
    public interface ITemperatureControl
    {
        public double T { get; }
        public void Inital();
        public void Update();
        public bool IsEnd();
        public bool Check(double score);
    }
}
