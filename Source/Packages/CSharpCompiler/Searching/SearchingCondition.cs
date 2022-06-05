using CSharpCompiler.Code;
using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingCondition : ISearchingObject
    {
        public IType Boolean;
        public ISearchingObject[] Nodes;
        public bool TrueBlock;
        public SearchingCondition(IType boolean, params ISearchingObject[] nodes) 
        { 
            Boolean = boolean;
            Nodes = nodes; 
        }
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => new();
        public SearchingResult Load() => new(new SearchingValue(Boolean,this));
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean) => new(this);
    }
}
