using Compiler.CSharp.Searching;

namespace Compiler.CSharp.Metadata
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
            Console.WriteLine($"\t\t\t\tBuilding Variable {Name}");
            IType[] types = top.Get(TypeFullName).Values.Where(value => value is SearchingNode_Type).Select(value => (value as SearchingNode_Type).Type).ToArray();
            if (types.Length == 0) Errors.Add($"Found Type Error {string.Join(".", TypeFullName)}");
            else if (types.Length == 1)
            {
                Type = types[0];
                Console.WriteLine($"\t\t\t\t\tFound Type {Type.Name}");
            }
            Console.WriteLine($"\t\t\t\tBuilded Variable {Name}");
        }
    }
}