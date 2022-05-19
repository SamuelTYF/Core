using Compiler.CSharp.Searching;

namespace Compiler.CSharp.Metadata
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
            Console.WriteLine($"\t\t\t\tBuilding Parameter {Name}");
            Type = top.GetType(TypeFullName);
            Console.WriteLine($"\t\t\t\t\tFound {Type.Name}");
            Console.WriteLine($"\t\t\t\tBuilded Parameter {Name}");
        }
        public void Lock(SearchingResult top) => Type = top.GetType(TypeFullName);
    }
}
