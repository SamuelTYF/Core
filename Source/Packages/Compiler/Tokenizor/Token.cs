namespace Compiler.Tokenizor
{
    public class Token
    {
        public string Type;
        public char Value;
        public Token(string type) => Type = type;
        public Token(char value) : this("Char") => Value = value;
        public override string ToString() => Value!=0? $"Type:\t{Type}\tValue:{Value.FromEscape()}":$"Type:\t{Type}";
    }
}
