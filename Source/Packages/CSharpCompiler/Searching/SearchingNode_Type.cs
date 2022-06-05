using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_Type : ISearchingObject
    {
        public IType Type;
        public SearchingNode_Type(IType type) => Type = type;
        public SearchingResult Call(SearchingResult[] parameters)=>new();

        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            if(Type.Fields.ContainsKey(name))
            {
                IField field = Type.Fields[name];
                if(field.Attributes.Contains("static"))
                    result.Add(new SearchingNode_Field(field));
            }
            if (Type.Properties.ContainsKey(name))
            {
                IProperty property = Type.Properties[name];
                result.Add(new SearchingNode_Property(property, this));
            }
            if (Type.Methods.ContainsKey(name))
                foreach (IMethod method in Type.Methods[name])
                    if (method.Attributes.Contains("static"))
                        result.Add(new SearchingNode_Method(method));
            return new(result);
        }
        public SearchingResult Load() => new();
        public SearchingResult Store(SearchingResult value) => new();
        public SearchingResult Condition(IType boolean) => new();
    }
}