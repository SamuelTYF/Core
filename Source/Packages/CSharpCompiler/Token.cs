using Compiler;

namespace CSharpCompiler
{
    public class Token
    {
        public string Type;
        public string? Value_String;
        public uint? Value_UInt32;
        public int? Value_Int32;
        public double? Value_Double;
        public char? Value_Char;
        public Token(string type)=>Type= type;
        public static Token Symbol(string value) => new("Symbol")
        {
            Value_String = value
        };
        public static Token String(string value) => new("String")
        {
            Value_String = value
        };
        public static Token UInt32(uint value) => new("UInt32")
        {
            Value_UInt32 = value
        };
        public static Token Int32(int value) => new("Int32")
        {
            Value_Int32 = value
        };
        public static Token Double(double value) => new("Double")
        {
            Value_Double = value
        };
        public static Token Char(char value) => new("Char")
        {
            Value_Char = value
        };
        public override string ToString()
        {
            string result = $"Type:{Type}";
            if (Value_String != null) result += $"\tString:{Value_String.FromEscape()}";
            if (Value_UInt32 != null) result += $"\tUInt32:{Value_UInt32}";
            if (Value_Int32 != null) result += $"\tInt32:{Value_Int32}";
            if (Value_Double != null) result += $"\tDouble:{Value_Double}";
            if (Value_Char != null) result += $"\tChar:{Value_Char}";
            return result;
        }
    }
}
