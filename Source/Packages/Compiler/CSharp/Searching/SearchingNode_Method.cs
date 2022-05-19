using Compiler.CSharp.Metadata;

namespace Compiler.CSharp.Searching
{
    public class SearchingNode_Method : ISearchingObject
    {
        public UserMethod Method;
        public SearchingNode_Method(UserMethod method)=>Method = method;
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> results = new();
            foreach (Parameter parameter in Method.Parameters)
                if (parameter.Name == name)
                    results.Add(new SearchingNode_Parameter(parameter));
            return new SearchingResult(results);
        }
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}
