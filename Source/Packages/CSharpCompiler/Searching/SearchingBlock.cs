using CSharpCompiler.Code;
using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingBlock : ISearchingObject
    {
        public ISearchingObject[] Nodes;
        public SearchingBlock(params ISearchingObject[] nodes)
        {
            Nodes = nodes;
        }
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => new();
        public SearchingResult Load() => new(this);
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean) => new();
    }
}
