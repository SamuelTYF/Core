using Compiler.CSharp.Searching;
using System.Xml.Linq;

namespace Compiler.CSharp.Metadata
{
    public class UserMethod:IMethod
    {
        public List<ICommand> Commands;
        public Dictionary<string, Variable> Variables;
        public bool IsAbstract;
        public bool Lambda;
        public List<string> Errors;
        public UserMethod():base()
        {
            Commands = new();
            Variables = new();
        }
        public void InsertParameter(Parameter parameter)
        {
            Parameters.Add(parameter);
            parameter.Errors = Errors;
        }
        public void RegisterVariable(string[] type,string name)
        {
            Variable variable = new(type, name);
            variable.Errors = Errors;
            if (!Variables.ContainsKey(name))
                Variables[name] = variable;
            else Errors.Add($"Insert Field Conflict {name}");
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
            string header=$"{string.Join(" ",Attributes)} {string.Join(".", ReturnTypeFullName)} {Name}({string.Join(",",Parameters)})";
            if (IsAbstract) codes.Add(header + ";");
            else if(Lambda)
            {
                codes.Add(header);
                codes.Add("\t=>" + Commands[0].ToString()+";");
            }
            else
            {
                codes.Add(header);
                codes.Add("{");
                codes.AddRange(Commands.Select(command => "\t" + string.Join("\n\t", command.ToString().Split("\n")) + ";"));
                codes.Add("}");
            }
            return string.Join("\n", codes);
        }
        public void Build(SearchingResult top)
        {
            Console.WriteLine($"\t\t\tBuilding Method {Name}");
            top.Values.Push(new SearchingNode_Method(this));
            foreach (Parameter parameter in Parameters)
                parameter.Build(top);
            foreach (Variable variable in Variables.Values)
                variable.Build(top);
            top.Values.Pop();
            foreach (ICommand command in Commands)
                command.Build(top);
            Console.WriteLine($"\t\t\tBuilded Method {Name}");
        }
    }
}
