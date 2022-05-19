using Compiler.CSharp.Metadata;

namespace Compiler.CSharp.Searching
{
    public class SearchingNode_Namespace: ISearchingObject
    {
        public INamespace Namespace;
        public SearchingNode_Namespace(INamespace @namespace)=>Namespace = @namespace;
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            INamespace @namespace = Namespace.GetNamespace(name);
            if (@namespace != null) result.Add(new SearchingNode_Namespace(@namespace));
            IType type = Namespace.GetType(name);
            if (type != null) result.Add(new SearchingNode_Type(type));
            return new(result);
        }
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
    }
}
