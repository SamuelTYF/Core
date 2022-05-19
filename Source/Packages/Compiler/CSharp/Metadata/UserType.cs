using Compiler.CSharp.Searching;
using System.Xml.Linq;

namespace Compiler.CSharp.Metadata
{
    public class UserType : IType
    {
        public List<string> Errors;
        public UserType() : base()
        {
            
        }
        public void InsertField(UserField field)
        {
            if(!Fields.ContainsKey(field.Name))
                Fields[field.Name] = field;
        }
        public void InsertMethod(UserMethod method)
        {
            if (!Methods.ContainsKey(method.Name))
                Methods[method.Name] = new();
            Methods[method.Name].Add(method);
            method.Errors = Errors;
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
        {
            List<string> codes = new();
            codes.Add($"{string.Join(" ",Attributes)} class {Name}");
            codes.Add("{");
            codes.AddRange(Fields.Values.Select(first =>"\t"+string.Join("\n\t",first.ToString().Split("\n"))));
            foreach (List<IMethod> methods in Methods.Values)
                foreach (IMethod method in methods)
                    codes.Add("\t" + string.Join("\n\t", method.ToString().Split("\n")));
            codes.Add("}");
            return string.Join("\n", codes);
        }
        public void Build(SearchingResult top)
        {
            Console.WriteLine($"\tBuilding Type {Name}");
            top.Values.Push(new SearchingNode_Type(this));
            foreach (UserField field in Fields.Values)
                field.Build(top);
            foreach (List<IMethod> methods in Methods.Values)
                foreach(UserMethod method in methods)
                    method.Build(top);
            top.Values.Pop();
            Console.WriteLine($"\tBuilded Type {Name}");
        }
    }
}
