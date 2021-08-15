using System;

namespace SAA
{
    public class TemperatureControl : ITemperatureControl
    {
        public double T { get;private set;  }
        public double Rate;
        public double TMax;
        public double TMin;
        public TemperatureControl(double rate,double tmax,double tmin)
        {
            Rate=rate;
            TMax=tmax;
            TMin=tmin;
        }
        public void Inital() => T = TMax;
        public void Update() => T *= Rate;
        public bool IsEnd() => T >= TMin;
        public bool Check(double score) => Math.Exp(score/T)>RandomHelper.NextDouble();
    }
}
