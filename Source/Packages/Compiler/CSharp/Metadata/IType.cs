namespace Compiler.CSharp.Metadata
{
    public class IType
    {
        public string? Name;
        public Dictionary<string, IType> NestTypes;
        public Dictionary<string, IField> Fields;
        public string? BaseTypeFullName;
        public Dictionary<string, List<IMethod>> Methods;
        public HashSet<string> Attributes;
        public IType()
        {
            NestTypes = new();
            Fields = new();
            Methods = new();
            Attributes = new();
        }
    }
}