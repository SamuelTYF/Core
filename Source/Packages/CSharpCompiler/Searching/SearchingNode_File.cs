using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_File : ISearchingObject
    {
        public ParsingFile File;
        public SearchingNode_File(ParsingFile file)=>File= file;
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => File.Namespaces.ContainsKey(name) ? new(new SearchingNode_Namespace(File.Namespaces[name])) : new();
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean) => new();
    }
}