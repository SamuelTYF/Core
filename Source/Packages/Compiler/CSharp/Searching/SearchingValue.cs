namespace Compiler.CSharp.Searching
{
    public class SearchingValue:ISearchingObject
    {
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name) => new();
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}
