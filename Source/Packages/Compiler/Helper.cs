namespace Compiler
{
    public static class Escape
    {
        public static string[] Values = new string[128];
        static Escape()
        {
            Values['\0'] = "\\0";
            Values['\u0001'] = "\\u0001";
            Values['\u0002'] = "\\u0002";
            Values['\u0003'] = "\\u0003";
            Values['\u0004'] = "\\u0004";
            Values['\u0005'] = "\\u0005";
            Values['\u0006'] = "\\u0006";
            Values['\u0007'] = "\\a";
            Values['\u0008'] = "\\b";
            Values['\u0009'] = "\\t";
            Values['\u000a'] = "\\n";
            Values['\u000b'] = "\\v";
            Values['\u000c'] = "\\f";
            Values['\u000d'] = "\\r";
            Values['\u000e'] = "\\u000e";
            Values['\u000f'] = "\\u000f";
            Values['\u0010'] = "\\u0010";
            Values['\u0011'] = "\\u0011";
            Values['\u0012'] = "\\u0012";
            Values['\u0013'] = "\\u0013";
            Values['\u0014'] = "\\u0014";
            Values['\u0015'] = "\\u0015";
            Values['\u0016'] = "\\u0016";
            Values['\u0017'] = "\\u0017";
            Values['\u0018'] = "\\u0018";
            Values['\u0019'] = "\\u0019";
            Values['\u001a'] = "\\u001a";
            Values['\u001b'] = "\\u001b";
            Values['\u001c'] = "\\u001c";
            Values['\u001d'] = "\\u001d";
            Values['\u001e'] = "\\u001e";
            Values['\u001f'] = "\\u001f";
            Values['\''] = "\\\'";
            Values['\\'] = "\\\\";
        }
        public static string FromEscape(this char value)
        {
            if (value >= Values.Length) return $"\\u{(int)value:x4}";
            else if (Values[value] == null) return value.ToString();
            else return Values[value];
        }
        public static string FromEscape(this string value)
            =>String.Join("", value.ToCharArray().Select(@char => @char.FromEscape()));
    }
}
