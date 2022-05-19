namespace Compiler.CSharp.Metadata
{
    public class Command_Constant<T> : ICommand
    {
        public string[] TypeFullName;
        public T Value;
        public Command_Constant(T value)
        {
            TypeFullName = typeof(T).GetFullName();
            Value = value;
        }
        public override string ToString() => $"{Value}";
    }
    public class Command_Constant_String: Command_Constant<string>
    {
        public Command_Constant_String(string value) : base(value) { }
        public override string ToString() => $"@\"{Value}\"";
    }
    public class Command_Constant_UInt32 : Command_Constant<uint>
    {
        public Command_Constant_UInt32(uint value) : base(value) { }
        public override string ToString() => $"0x{Value:X}";
    }
    public class Command_Constant_Char : Command_Constant<char>
    {
        public Command_Constant_Char(char value) : base(value) { }
        public override string ToString() => $"'{Value.FromEscape()}'";
    }
}
