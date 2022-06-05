namespace CSharpCompiler.Metadata
{
    public class IMethod
    {
        public string[] ReturnTypeFullName;
        public string? Name;
        public List<Parameter> Parameters;
        public HashSet<string> Attributes;
        public IType ReturnType;
        public IMethod()
        {
            Parameters = new();
            Attributes = new();
        }
        public string BaseString()
            => $"{string.Join(".", ReturnTypeFullName)} {Name} ({string.Join(",", Parameters.Select(parameter => string.Join(".", parameter.TypeFullName)))})";
    }
}