using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public interface ISearchingObject
    {
        public SearchingResult Load();
        public SearchingResult Store(SearchingResult value);
        public SearchingResult Get(string name);
        public SearchingResult Call(SearchingResult[] parameters);
        public SearchingResult CallVirt(SearchingResult[] parameters);
        public SearchingResult Condition(IType boolean);
    }
}
