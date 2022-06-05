using CSharpCompiler.Searching;
using System.Net.Sockets;

namespace CSharpCompiler.Metadata
{
    public class Parameter
    {
        public string[] TypeFullName;
        public string? Name;
        public HashSet<string> Attributes;
        public IType Type;
        public List<string> Errors;
        public Parameter(string[] typefullname, string name)
        {
            TypeFullName = typefullname;
            Name = name;
            Attributes = new();
        }
        public override string ToString()
        {
            return $"{string.Join(".", TypeFullName)} {Name}";
        }
        public void Build(SearchingResult top)
        {
            Type = top.GetType(TypeFullName);
            if(Type==null)
                Errors.Add($"Can't Found {Type.Name}");
        }
        public void Lock(SearchingResult top)=>Type = top.GetType(TypeFullName);
    }
}
