using Compiler.CSharp.Metadata;

namespace Compiler.CSharp.Searching
{
    public class SearchingNode_Parameter : ISearchingObject
    {
        public Parameter Parameter;
        public SearchingNode_Parameter(Parameter parameter) => Parameter = parameter;
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => new();
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}
