using Compiler.CSharp.Searching;

namespace Compiler.CSharp.Metadata
{
    public class UserNamespace: INamespace
    {
        public string? Name;
        public Dictionary<string, UserNamespace> Namespaces;
        public Dictionary<string, UserType> Types;
        public List<string> Errors;
        public UserNamespace()
        {
            Namespaces = new();
            Types = new();
        }
        public void CombineWith(UserNamespace @namespace)
        {
            foreach (KeyValuePair<string, UserNamespace> value in @namespace.Namespaces)
                if (Namespaces.ContainsKey(value.Key))
                    Namespaces[value.Key].CombineWith(value.Value);
                else Namespaces[value.Key] = value.Value;
            foreach (KeyValuePair<string, UserType> value in @namespace.Types)
                Types[value.Key] = value.Value;
        }
        public INamespace GetNamespace(string name)
            => Namespaces.ContainsKey(name) ? Namespaces[name] : null;
        public IType GetType(string name)
            => Types.ContainsKey(name) ? Types[name] : null;
        public void InsertType(UserType type)
        {
            if (!Types.ContainsKey(type.Name))
                Types[type.Name] = type;
        }
        public override string ToString()
        {
            List<string> codes = new();
            if(Types.Count>0)
            {
                codes.Add($"namespace {Name}");
                codes.Add("{");
                codes.AddRange(Types.Values.Select(type => "\t"+string.Join("\n\t", type.ToString().Split("\n"))));
                codes.Add("}");
            }
            codes.AddRange(Namespaces.Values.Select(@namespace => @namespace.ToString()));
            return string.Join("\n", codes);
        }
        public void Build(SearchingResult top)
        {
            Console.WriteLine($"Building Namespace {Name}");
            top.Values.Push(new SearchingNode_Namespace(this));
            foreach (UserNamespace @namespace in Namespaces.Values)
                @namespace.Build(top);
            foreach (UserType type in Types.Values)
                type.Build(top);
            top.Values.Pop();
            Console.WriteLine($"Builded Namespace {Name}");
        }
    }
}
