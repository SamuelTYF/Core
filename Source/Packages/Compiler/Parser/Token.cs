namespace Compiler.Parser
{
    public class Token
    {
        public string Type;
        public Symbol? Value_Symbol;
        public Token(string type) => Type = type;
        public static Token Variable(string name) => new("Symbol")
        {
            Value_Symbol=Symbol.Variable(name)
        };
        public static Token Terminal(string name) => new("Symbol")
        {
            Value_Symbol = Symbol.Terminal(name)
        };
    }
}
