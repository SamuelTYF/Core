using CSharpCompiler.Metadata;

namespace CSharpCompiler.Searching
{
    public class SearchingNode_Variable : ISearchingObject
    {
        public Variable Variable;
        public SearchingNode_Variable(Variable variable) => Variable = variable;
        public SearchingResult Call(SearchingResult[] parameters) => new();

        public SearchingResult CallVirt(SearchingResult[] parameters) => new();
        public SearchingResult Get(string name)
        {
            List<ISearchingObject> result = new();
            IType Type = Variable.Type;
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
        public SearchingResult InstanceGet() => new();
        public SearchingResult Load() => new(new SearchingValue(Variable.Type,this));
        public SearchingResult Store(SearchingResult value) => new(
            value.Values.Where(node => (node as SearchingValue).Type == Variable.Type)
            .Select(node => new SearchingValue(Variable.Type, this, node)
            ));
        public SearchingResult Condition(IType boolean)
        {
            if (Variable.Type == boolean)
                return new(new SearchingCondition(boolean, this));
            else return new();
        }
    }
}
