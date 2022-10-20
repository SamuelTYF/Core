namespace Json.Grammar
{
    public class Token
    {
        public string Type;
        public string? Value;
        public Token(string type,string? value=null)
        {
            Type = type;
            Value = value;
        }
        public override string ToString() => $"{Type}\t{Value}";
    }
}
