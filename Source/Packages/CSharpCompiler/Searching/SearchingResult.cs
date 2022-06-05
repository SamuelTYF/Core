using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingResult: ISearchingObject
    {
        public Stack<ISearchingObject> Values;
        public SearchingResult(params ISearchingObject[] values) => Values = new(values);
        public SearchingResult(IEnumerable<ISearchingObject> values) => Values = new(values);
        public SearchingResult Call(SearchingResult[] parameters)
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.Call(parameters).Values);
            return new(results);
        }
        public SearchingResult CallVirt(SearchingResult[] parameters)
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.CallVirt(parameters).Values);
            return new(results);
        }
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.Get(name).Values);
            return new(results);
        }
        public SearchingResult Get(string[] names)
        {
            SearchingResult result = this;
            foreach (string name in names)
                result = result.Get(name);
            return result;
        }
        public SearchingResult Load()
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.Load().Values);
            return new(results);
        }
        public SearchingResult Store(SearchingResult value)
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.Store(value).Values);
            return new(results);
        }
        public IType GetType(params string[] names)
        {
            IType[] types = Get(names).Values.Where(value => value is SearchingNode_Type).Select(value => (value as SearchingNode_Type).Type).ToArray();
            return types.Length == 1 ? types[0] : null;
        }
        public SearchingResult Condition(IType boolean)
        {
            List<ISearchingObject> results = new();
            foreach (ISearchingObject temp in Values)
                results.AddRange(temp.Condition(boolean).Values);
            return new(results);
        }
    }
}
