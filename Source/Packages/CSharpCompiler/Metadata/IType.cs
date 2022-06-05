namespace CSharpCompiler.Metadata
{
    public class IType
    {
        public string? Name;
        public Dictionary<string, IType> NestTypes;
        public Dictionary<string, IField> Fields;
        public Dictionary<string, IProperty> Properties;
        public string[] BaseTypeFullName;
        public IType BaseType;
        public Dictionary<string, List<IMethod>> Methods;
        public HashSet<string> Attributes;
        public IType()
        {
            NestTypes = new();
            Fields = new();
            Properties = new();
            Methods = new();
            Attributes = new();
        }
        public bool Is(IType type)
        {
            IType t = this;
            while(t!=null)
            {
                if (t == type) return true;
                t = t.BaseType;
            }
            return false;
        }
    }
}