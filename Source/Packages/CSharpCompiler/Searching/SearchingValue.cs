using CSharpCompiler.Code;
using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingValue:ISearchingObject
    {
        public IType Type;
        public ISearchingObject[] Nodes;
        public SearchingValue(IType type, params ISearchingObject[] nodes)
        {
            Type = type;
            Nodes = nodes;
        }
        public SearchingResult Call(SearchingResult[] parameters) => new();
        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            if (Type.Fields.ContainsKey(name))
            {
                IField field = Type.Fields[name];
                if (!field.Attributes.Contains("static"))
                    result.Add(new SearchingNode_Field(field,this));
            }
            if (Type.Methods.ContainsKey(name))
                foreach (IMethod method in Type.Methods[name])
                    if (!method.Attributes.Contains("static"))
                        result.Add(new SearchingNode_Method(method,this));
            return new(result);
        }
        public SearchingResult Load() => new(this);
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean)
        {
            if (Type == boolean)
                return new(new SearchingCondition(boolean, this));
            else return new();
        }
    }
}
