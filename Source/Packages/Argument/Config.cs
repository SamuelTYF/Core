using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argument
{
    public delegate bool ParserFunction<T>(string arg, out T value);
    public abstract class IConfig
    {
        public string Name;
        public string[] Params;
        public string Description;
        public bool Success;
        public int Count;
        public bool NeedValue;
        public IConfig(string name,string[] ps,string description,int count,bool needvalue)
        {
            Name = name;
            Params = ps;
            Description = description;
            Success = false;
            Count = count;
            NeedValue = needvalue;
        }
        public abstract bool Parse(string[] arg,ref int index);
    }
    public class Config : IConfig
    {
        public Config(string name, string[] ps, string description, bool needvalue)
            : base(name, ps, description, 0, needvalue) 
        {
        }
        public override bool Parse(string[] arg, ref int index) => Success = true;
    }
    public class ConfigInt: IConfig
    {
        public int Value;
        public ConfigInt(string name, string[] ps, string description, bool needvalue)
            : base(name, ps, description, 1,needvalue)
            => Success = false;
        public override bool Parse(string[] arg, ref int index) => Success = int.TryParse(arg[index++], out Value);
    }
    public class ConfigString : IConfig
    {
        public string Value;
        public ConfigString(string name, string[] ps, string description, bool needvalue)
            : base(name, ps, description, 1,needvalue)
            => Success = false;
        public override bool Parse(string[] arg, ref int index)
        {
            Value=arg[index++];
            return Success = true;
        }
    }
    public class Config<T>:IConfig
    {
        public T Value;
        public ParserFunction<T> Function;
        public Config(string name, string[] ps, string description, int count, ParserFunction<T> function, bool needvalue)
            :base(name, ps, description, count,needvalue)
            =>Function = function;
        public override bool Parse(string[] arg, ref int index)
            => Success = Function(arg[index++], out Value);
    }
    public class Config<T1, T2> : IConfig
    {
        public T1 Value1;
        public T2 Value2;
        public ParserFunction<T1> Function1;
        public ParserFunction<T2> Function2;
        public Config(string name, string[] ps, string description, int count, ParserFunction<T1> function1, ParserFunction<T2> function2, bool needvalue)
        :base(name, ps, description, count,needvalue)
        {
            Function1 = function1;
            Function2 = function2;
        }
        public override bool Parse(string[] arg, ref int index)
            => Success = Function1(arg[index++], out Value1) && Function2(arg[index++], out Value2);
    }
}
