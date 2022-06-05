using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class ParsingFile
    {
        public Dictionary<string, string[]> Usings;
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
            codes.AddRange(Usings.Keys.Select(name => $"using {name};"));
            codes.AddRange(Namespaces.Values.Select(@namespace => @namespace.ToString()));
            return string.Join("\n",codes);
        }
        public void Build(SearchingResult top)
        {
            top.Values.Push(new SearchingNode_File(this));
            foreach (KeyValuePair<string,string[]> value in Usings)
            {
                SearchingResult result = top.Get(value.Value);
                if (result.Values.Count != 1) Console.WriteLine($"Can't Found {value.Key}");
                else
                {
                    SearchingNode_Namespace @namespace = result.Values.Peek() as SearchingNode_Namespace;
                    if(@namespace==null) Console.WriteLine($"Can't Found {value.Key}");
                    top.Values.Push(@namespace);
                }
            }
            foreach (UserNamespace @namespace in Namespaces.Values)
                @namespace.Build(top);
            foreach (UserNamespace @namespace in Namespaces.Values)
                @namespace.BuildCommand(top);
            top.Values.Pop();
        }
        public IEnumerable<UserType> GetTypes()
        {
            foreach (UserNamespace @namespace in Namespaces.Values)
                foreach (UserType type in @namespace.GetTypes())
                    yield return type;
        }
    }
}
