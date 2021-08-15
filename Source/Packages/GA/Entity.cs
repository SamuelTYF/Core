using System;

namespace GA
{
    public class Entity<TCode,TValue>:IComparable<Entity<TCode, TValue>> where TCode : ICode
    {
        public TCode Code;
        public TValue Value;
        public double Score;
        public Entity(TCode code,TValue value,double score)
        {
            Code = code;
            Value = value;
            Score = score;
        }
        public Entity(TCode code,ICodeFactory<TCode,TValue> factory,Assess<TValue> assess)
        {
            Code = code;
            Value = factory.Decode(code);
            Score = assess(Value);
        }
        public int CompareTo(Entity<TCode, TValue>? other) =>other==null?throw new Exception():Score.CompareTo(other.Score);
    }
}
