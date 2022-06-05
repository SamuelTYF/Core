using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class UserField:IField
    {
        public List<string> Errors;
        public UserField() : base()
        {
        }
        public void UpdateAttributes(List<string> attributes)
        {
            foreach (string attribute in attributes)
                if (Attributes.Contains(attribute)) Errors.Add($"Type Attribute Conflicted {attribute}");
                else Attributes.Add(attribute);
            int count = 0;
            if (Attributes.Contains("public")) count++;
            if (Attributes.Contains("private")) count++;
            if (Attributes.Contains("internal")) count++;
            if (count > 1) Errors.Add($"Type Attributes Conflict");
            else if (count == 0) Attributes.Add("private");
        }
        public override string ToString()
            => $"{string.Join(" ",Attributes)} {string.Join(".", TypeFullName)} {Name};";
        public void Build(SearchingResult top)
        {
            Type = top.GetType(TypeFullName);
            if(Type==null)
                Errors.Add($"Can't Found Type {Type.Name}");
        }
    }
}
