using Compiler.CSharp.Metadata;
using System.Reflection;

namespace Compiler.CSharp.Searching
{
    public class SearchingNode_Root : ISearchingObject
    {
        public Dictionary<string, InternalNamespace> Namespaces;
        private Dictionary<string, InternalNamespace> _BufferNamespaces;
        public SearchingNode_Root()
        {
            Namespaces = new();
            _BufferNamespaces = new();
        }
        public InternalNamespace GetNamespace(string name)
        {
            if (_BufferNamespaces.ContainsKey(name)) return _BufferNamespaces[name];
            string[] names = name.Split('.');
            if (!Namespaces.ContainsKey(names[0]))
                Namespaces[names[0]] = new(names[0]);
            InternalNamespace @namespace = Namespaces[names[0]].GetNamespace(names, 1);
            _BufferNamespaces[@namespace.Name] = @namespace;
            return @namespace;
        }
        public void LoadAssembly(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
                foreach (Type type in assembly.GetTypes())
                    if (type.IsPublic && !type.IsNested)
                        GetNamespace(type.Namespace).Types[type.Name] = new InternalType(type);
        }
        public void Lock()
        {
            SearchingResult top = new(this);
            foreach (InternalNamespace @namespace in Namespaces.Values)
                @namespace.Lock(top);
        }
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => Namespaces.ContainsKey(name) ? new(new SearchingNode_Namespace(Namespaces[name])) : new();
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}