namespace CSharpCompiler.Metadata
{
    public class IField
    {
        public string? Name;
        public string[] TypeFullName;
        public HashSet<string> Attributes;
        public IType Type;
        public IField()
        {
            Name = null;
            TypeFullName = Array.Empty<string>();
            Attributes = new();
        }
    }
}