using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class InternalNamespace:INamespace
    {
        public string Name;
        public Dictionary<string, InternalNamespace> Namespaces;
        public Dictionary<string, InternalType> Types;
        public InternalNamespace(string name)
        {
            Name = name;
            Namespaces = new();
            Types = new();
        }
        public InternalNamespace GetNamespace(string[] names,int index)
        {
            if (names.Length == index) return this;
            else
            {
                if (!Namespaces.ContainsKey(names[index]))
                    Namespaces[names[index]] = new($"{Name}.{names[index]}");
                return Namespaces[names[index]].GetNamespace(names, index + 1);
            }
        }
        public INamespace GetNamespace(string name)
            => Namespaces.ContainsKey(name) ? Namespaces[name] : null;
        public IType GetType(string name)
            => Types.ContainsKey(name) ? Types[name] : null;
        public void Lock(SearchingResult top)
        {
            foreach (InternalNamespace @namespace in Namespaces.Values)
                @namespace.Lock(top);
            foreach (InternalType type in Types.Values)
                type.Lock(top);
        }
    }
}
