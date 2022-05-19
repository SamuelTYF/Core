using Compiler.CSharp.Metadata;

namespace Compiler.CSharp.Searching
{
    public class SearchingNode_Type : ISearchingObject
    {
        public IType Type;
        public SearchingNode_Type(IType type) => Type = type;
        public SearchingResult Call(SearchingResult[] parameters)=>new();
        public SearchingResult Get(string name) => new();
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}