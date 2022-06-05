namespace CSharpCompiler.Code
{
    public class ILCode_Operator2:ILCode
    {
        public static readonly Dictionary<string, string> Map;
        static ILCode_Operator2()
        {
            Map = new();
            Map["+"] = "plus";
            Map["-"] = "sub";
            Map["*"] = "mul";
            Map["/"] = "div";
            Map["%"] = "rem";
            Map["<"] = "clt";
            Map[">"] = "cgt";
            Map["=="] = "ceq";
        }
        public string Operator;
        public ILCode_Operator2(ILCode_Block parent,string key) : base(parent)
        {
            Operator = Map[key];
        }
        public override int GetLength() => 2;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\t{Operator}";
    }
}
