using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System.Xml.Linq;

namespace CSharpCompiler.Metadata
{
    public class UserMethod:IMethod
    {
        public List<ICommand> Commands;
        public Dictionary<string, Variable> Variables;
        public bool IsAbstract;
        public bool Lambda;
        public List<string> Errors;
        public ILCode_Block Code;
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
            top.Values.Push(new SearchingNode_Method(this));
            ReturnType = top.GetType(ReturnTypeFullName);
            foreach (Parameter parameter in Parameters)
                parameter.Build(top);
            foreach (Variable variable in Variables.Values)
                variable.Build(top);
            top.Values.Pop();
        }
        public void BuildCommand(SearchingResult top)
        {
            List<ISearchingObject> nodes = new();
            top.Values.Push(new SearchingNode_Method(this));
            bool success = true;
            foreach (ICommand command in Commands)
            {
                SearchingResult result = command.Build(top).Load();
                if (result.Values.Count == 1)
                    nodes.Add(result.Values.Peek());
                else
                {
                    Errors.Add($"\tError\t{command}");
                    success = false;
                }
            }
            if (!success) return;
            top.Values.Pop();
            ILCode_Block code = new(null);
            ILCode_Block[] blocks = new ILCode_Block[Commands.Count + 1];
            for (int i = 0; i < blocks.Length; i++)
                blocks[i] = new(code);
            new ILCode_Ret(blocks[^1]);
            for(int i=0;i<Commands.Count;i++)
                Commands[i].GetInstruction(nodes[i], blocks[i], blocks[i+1]);
            code.Update();
            Code = code;
        }
        public string ILFormat()
        {
            string local = 
                $".local init(\n\t" +
                string.Join(",\n\t",Variables.Values.Select(variable=>$"{string.Join(".",variable.TypeFullName)}\t{variable.Name}"))+
                $"\n)";
            string commands = Code.ToString();
            string header = $"{string.Join(" ", Attributes)} {string.Join(".", ReturnTypeFullName)} {Name}({string.Join(",", Parameters)})";
            return header+"\n"+local+"\n"+commands;
        }
    }
}
