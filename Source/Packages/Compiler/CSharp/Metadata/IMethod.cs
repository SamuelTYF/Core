namespace Compiler.CSharp.Metadata
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
    }
}