using Compiler;
using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
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
        public virtual SearchingResult Build(SearchingResult top)
        {
            IType type = top.GetType(TypeFullName);
            if (type == null) return new();
            return new(new SearchingValue(type));
        }
        public virtual void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc<T>(current, Value);
        }
    }
    public class Command_Constant_String: Command_Constant<string>
    {
        public Command_Constant_String(string value) : base(value) { }
        public override string ToString() => $"\"{Value.FromEscape()}\"";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc_str(current, Value);
        }
    }
    public class Command_Constant_UInt32 : Command_Constant<uint>
    {
        public Command_Constant_UInt32(uint value) : base(value) { }
        public override string ToString() => $"0x{Value:X}";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc_i4(current, (int)Value);
        }
    }
    public class Command_Constant_Int32 : Command_Constant<int>
    {
        public Command_Constant_Int32(int value) : base(value) { }
        public override string ToString() => $"0x{Value:X}";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc_i4(current, Value);
        }
    }
    public class Command_Constant_Char : Command_Constant<char>
    {
        public Command_Constant_Char(char value) : base(value) { }
        public override string ToString() => $"'{Value.FromEscape()}'";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc_i4(current, Value);
        }
    }
    public class Command_Constant_Double : Command_Constant<double>
    {
        public Command_Constant_Double(double value) : base(value) { }
        public override string ToString() => $"{Value}";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            new ILCode_Ldc_r8(current, Value);
        }
    }
    public class Command_Constant_Boolean : Command_Constant<bool>
    {
        public Command_Constant_Boolean(bool value) : base(value) { }
        public override string ToString() => $"{Value}";
        public override void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingCondition sc)
            {
                if (sc.TrueBlock==Value) 
                    new ILCode_Br(current, Br_Operator.br, next);
            }
            else if (node is SearchingValue)
            {
                new ILCode_Ldc_i4(current, Value ? 1 : 0);
            }
            else throw new Exception();
        }
        public override SearchingResult Build(SearchingResult top)
        {
            IType type = top.GetType("System","Boolean");
            if (type == null) return new();
            return new(new SearchingCondition(type));
        }
    }
}
