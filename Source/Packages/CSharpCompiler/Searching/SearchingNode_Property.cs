using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_Property : ISearchingObject
    {
        public IProperty Property;
        public ISearchingObject[] Nodes;
        public SearchingNode_Property(IProperty property, params ISearchingObject[] nodes)
        {
            Property = property;
            Nodes = nodes;
        }
        public SearchingResult Call(SearchingResult[] parameters)=>new();
        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            if (Property.Getter == null || !Property.Getter.Attributes.Contains("public") || Property.Getter.Parameters.Count > 0) return new();
            IType Type = Property.Type;
            if (Type.Fields.ContainsKey(name))
            {
                IField field = Type.Fields[name];
                if (!field.Attributes.Contains("static"))
                    result.Add(new SearchingNode_Field(field, this));
            }
            if (Type.Properties.ContainsKey(name))
            {
                IProperty property = Type.Properties[name];
                result.Add(new SearchingNode_Property(property,this));
            }
            if (Type.Methods.ContainsKey(name))
                foreach (IMethod method in Type.Methods[name])
                    if (!method.Attributes.Contains("static"))
                        result.Add(new SearchingNode_Method(method, this));
            return new(result);
        }
        public SearchingResult Load()
        {
            if (Property.Getter == null || !Property.Getter.Attributes.Contains("public") || Property.Getter.Parameters.Count > 0) return new();
            return new(new SearchingValue(Property.Type, this));
        }
        public SearchingResult Store(SearchingResult value)
        {
            if (Property.Setter == null || !Property.Setter.Attributes.Contains("public") || Property.Setter.Parameters.Count > 0) return new();
            return new(new SearchingValue(Property.Type, this));
        }
        public SearchingResult Condition(IType boolean)
        {
            if (Property.Getter == null || !Property.Getter.Attributes.Contains("public") || Property.Getter.Parameters.Count > 0|| Property.Type != boolean) return new();
            return new(new SearchingValue(Property.Type, this));
        }
    }
}