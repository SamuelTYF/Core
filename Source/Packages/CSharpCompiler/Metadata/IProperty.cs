
namespace CSharpCompiler.Metadata
{
    public class IProperty
    {
        public string? Name;
        public string[] TypeFullName;
        public HashSet<string> Attributes;
        public IType Type;
        public IMethod Setter;
        public IMethod Getter;
        public IProperty()
        {
            Name = null;
            TypeFullName = Array.Empty<string>();
            Attributes = new();
        }
    }
}
