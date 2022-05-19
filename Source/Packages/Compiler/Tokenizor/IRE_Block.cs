using Compiler.Tokenizor.Automata;

namespace Compiler.Tokenizor
{
    public interface IRE_Block
    {
        public void Register(ENFA enfa,int start,int end);
    }
    public class RE_Char: IRE_Block
    {
        public char Value;
        public int Token = 0;
        public RE_Char(char value)=>Value = value;
        public override string ToString() => Value.FromEscape();
        public void Register(ENFA enfa, int start, int end)
            =>enfa.InsertDelta(start, Token, end);
    }
    public class RE_CharSet : IRE_Block
    {
        public List<CharRange> Values;
        public HashSet<int> Tokens = new();
        public RE_CharSet() => Values = new();
        public void Insert(char l, char r) => Values.Add(new(l, r));
        public void Insert(char l) => Values.Add(new(l, l));
        public override string ToString() => $"[{string.Join("", Values)}]";
        public void Register(ENFA enfa, int start, int end)
        {
            foreach (int token in Tokens)
                enfa.InsertDelta(start, token, end);
        }
    }
    public class RE_Block : IRE_Block
    {
        public List<IRE_Block> Values;
        public RE_Block() => Values = new();
        public override string ToString() => string.Join("", Values);
        public void Register(ENFA enfa, int start, int end)
        {
            if (Values.Count == 0) enfa.InsertDelta(start, null, end);
            int l = start;
            for(int i=0;i<Values.Count;i++)
                if (i != Values.Count - 1)
                {
                    int r = enfa.RegisterVariable();
                    Values[i].Register(enfa, l, r);
                    l = r;
                }
                else Values[i].Register(enfa, l, end);
        }
    }
    public class RE_Optional : IRE_Block
    {
        public List<IRE_Block> Values;
        public RE_Optional() => Values = new();
        public override string ToString() => $"({string.Join("|", Values)})";
        public void Register(ENFA enfa, int start, int end)
        {
            foreach (IRE_Block re in Values)
                re.Register(enfa, start, end);
        }
    }
    public class RE_Closure:IRE_Block
    {
        public IRE_Block Value;
        public RE_Closure(IRE_Block value)=>Value = value;
        public override string ToString() => $"{Value}*";
        public void Register(ENFA enfa, int start, int end)
        {
            int t = enfa.RegisterVariable();
            enfa.InsertDelta(start,null, t);
            enfa.InsertDelta(t, null, end);
            Value.Register(enfa, t, t);
        }
    }
    public class RE_PositiveClosure : IRE_Block
    {
        public IRE_Block Value;
        public RE_PositiveClosure(IRE_Block value) => Value = value;
        public override string ToString() => $"{Value}+";
        public void Register(ENFA enfa, int start, int end)
        {
            int l=enfa.RegisterVariable();
            int r=enfa.RegisterVariable();
            enfa.InsertDelta(start, null, l);
            enfa.InsertDelta(r, null, l);
            enfa.InsertDelta(r, null, end);
            Value.Register(enfa, l, r);
        }
    }
}
