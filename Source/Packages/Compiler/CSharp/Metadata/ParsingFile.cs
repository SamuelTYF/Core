using Compiler.CSharp.Searching;

namespace Compiler.CSharp.Metadata
{
    public class ParsingFile
    {
        public HashSet<string> Usings;
        public Dictionary<string,UserNamespace> Namespaces;
        public List<string> Errors;
        public ParsingFile()
        {
            Usings = new();
            Namespaces = new();
            Errors = new();
        }
        public void InsertNamespace(UserNamespace @namespace)
        {
            @namespace.Errors = Errors;
            if (!Namespaces.ContainsKey(@namespace.Name))
                Namespaces[@namespace.Name] = @namespace;
            else Namespaces[@namespace.Name].CombineWith(@namespace);
        }
        public override string ToString()
        {
            List<string> codes = new();
            codes.AddRange(Usings.Select(name => $"using {name};"));
            codes.AddRange(Namespaces.Values.Select(@namespace => @namespace.ToString()));
            return string.Join("\n",codes);
        }
        public void Build(SearchingResult top)
        {
            top.Values.Push(new SearchingNode_File(this));
            foreach (UserNamespace @namespace in Namespaces.Values)
                @namespace.Build(top);
            top.Values.Pop();
        }
    }
}
