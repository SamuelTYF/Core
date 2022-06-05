using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_Method : ISearchingObject
    {
        public IMethod Method;
        public ISearchingObject[] Nodes;
        public SearchingNode_Method(IMethod method, params ISearchingObject[] nodes)
        {
            Method = method;
            Nodes = nodes;
        }
        public SearchingResult Call(SearchingResult[] parameters)
        {
            SearchingValue[] values = parameters
                .Where(parameter => parameter.Values.Count == 1)
                .Select(parameter => parameter.Values.Peek())
                .Where(value => value is SearchingValue)
                .Select(value => value as SearchingValue)
                .ToArray();
            if (Method.Parameters.Count != values.Length) return new();
            for (int i = 0; i < values.Length; i++)
                if (values[i].Type!=Method.Parameters[i].Type) return new();
            List<ISearchingObject> nodes = new();
            nodes.Add(this);
            nodes.AddRange(values);
            return new(new SearchingValue(Method.ReturnType, nodes.ToArray()));
        }
        public SearchingResult CallVirt(SearchingResult[] parameters)
        {
            SearchingValue[] values = parameters
                .Where(parameter => parameter.Values.Count == 1)
                .Select(parameter => parameter.Values.Peek())
                .Where(value => value is SearchingValue)
                .Select(value => value as SearchingValue)
                .ToArray();
            if (Method.Parameters.Count != values.Length) return new();
            for (int i = 0; i < values.Length; i++)
                if (!values[i].Type.Is(Method.Parameters[i].Type)) return new();
            List<ISearchingObject> nodes = new();
            nodes.Add(this);
            nodes.AddRange(values);
            return new(new SearchingValue(Method.ReturnType, nodes.ToArray()));
        }
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> results = new();
            foreach (Parameter parameter in Method.Parameters)
                if (parameter.Name == name)
                    results.Add(new SearchingNode_Parameter(parameter));
            if (Method is UserMethod method)
                if (method.Variables.ContainsKey(name))
                    results.Add(new SearchingNode_Variable(method.Variables[name]));
            return new SearchingResult(results);
        }
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean) => new();
    }
}
