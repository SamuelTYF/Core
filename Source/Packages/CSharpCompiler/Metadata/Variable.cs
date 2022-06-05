using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Variable
    {
        public string[] TypeFullName;
        public string Name;
        public IType Type;
        public List<string> Errors;
        public Variable(string[] typeFullName, string name)
        {
            TypeFullName = typeFullName;
            Name = name;
        }
        public override string ToString()
            => $"{string.Join(".", TypeFullName)} {Name}";
        public void Build(SearchingResult top)
        {
            IType[] types = top.Get(TypeFullName).Values.Where(value => value is SearchingNode_Type).Select(value => (value as SearchingNode_Type).Type).ToArray();
            if (types.Length == 1)Type = types[0];
            else Errors.Add($"Found Type Error {string.Join(".", TypeFullName)}");
        }
    }
}