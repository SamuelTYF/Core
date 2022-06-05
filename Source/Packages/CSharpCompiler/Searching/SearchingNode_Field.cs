using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_Field : ISearchingObject
    {
        public IField Field;
        public ISearchingObject[] Nodes;
        public SearchingNode_Field(IField field, params ISearchingObject[] nodes)
        {
            Field = field;
            Nodes = nodes;
        }
        public SearchingResult Call(SearchingResult[] parameters)=>new();

        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            IType Type = Field.Type;
            if (Type.Fields.ContainsKey(name))
            {
                IField field = Type.Fields[name];
                if (!field.Attributes.Contains("static"))
                    result.Add(new SearchingNode_Field(field, this));
            }
            if (Type.Properties.ContainsKey(name))
            {
                IProperty property = Type.Properties[name];
                result.Add(new SearchingNode_Property(property, this));
            }
            if (Type.Methods.ContainsKey(name))
                foreach (IMethod method in Type.Methods[name])
                    if (!method.Attributes.Contains("static"))
                        result.Add(new SearchingNode_Method(method, this));
            return new(result);
        }
        public SearchingResult Load() => new(new SearchingValue(Field.Type,this));
        public SearchingResult Store(SearchingResult value) => new(new SearchingValue(Field.Type,this, value));
        public SearchingResult Condition(IType boolean)
        {
            if (Field.Type== boolean)
                return new(new SearchingCondition(boolean, this));
            else return new();
        }
    }
}