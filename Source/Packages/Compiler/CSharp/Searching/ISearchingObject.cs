namespace Compiler.CSharp.Searching
{
    public interface ISearchingObject
    {
        public SearchingResult Load();
        public SearchingResult Store(SearchingResult value);
        public SearchingResult InstanceGet();
        public SearchingResult Get(string name);
        public SearchingResult Call(SearchingResult[] parameters);
    }
}
