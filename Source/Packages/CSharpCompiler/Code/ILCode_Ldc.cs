using Compiler;

namespace CSharpCompiler.Code
{
    public class ILCode_Ldc<T> : ILCode
    {
        public T Value;
        public ILCode_Ldc(ILCode_Block parent, T value) : base(parent) => Value = value;
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value,16).PadLeft(4,'0')}:\tldc\t{Value}";
    }
    public class ILCode_Ldc_i4 : ILCode_Ldc<int>
    {
        public ILCode_Ldc_i4(ILCode_Block parent, int value) : base(parent, value){}
        public override int GetLength() => 5;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\tldc.i4\t{Value}";
    }
    public class ILCode_Ldc_r8 : ILCode_Ldc<double>
    {
        public ILCode_Ldc_r8(ILCode_Block parent, double value) : base(parent, value) { }
        public override int GetLength() => 9;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\tldc.r8\t{Value}";
    }
    public class ILCode_Ldc_str : ILCode_Ldc<string>
    {
        public ILCode_Ldc_str(ILCode_Block parent, string value) : base(parent, value) { }
        public override int GetLength() => 5;
        public override string ToString() => $"IL_{Convert.ToString(Offset.Value, 16).PadLeft(4, '0')}:\tldc.str\t\"{Value.FromEscape()}\"";
    }
}
