using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_Load:ICommand
    {
        public string[] Names;
        public Command_Load(params string[] names)=>Names= names;
        public SearchingResult Build(SearchingResult top)
            => top.Get(Names);

        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingNode_Method method)
            {
                if (method.Nodes.Length == 0) return;
                else
                {
                    new _Command_Load(Names, Names.Length - 2).GetInstruction(method.Nodes[0], current, next);
                }
            }
            else if (node is SearchingNode_Variable variable) return;
            else if(node is SearchingValue value)
            {
                ISearchingObject n = value.Nodes[0];
                new _Command_Load(Names,Names.Length-1).GetInstruction(n, current, next);
            }
            else throw new Exception();
        }

        public override string ToString() => string.Join(".", Names);
    }
    public class _Command_Load : ICommand
    {
        public string[] Names;
        public int Index;
        public _Command_Load(string[] names,int index)
        {
            Names = names;
            Index = index;
        }
        public SearchingResult Build(SearchingResult top) => throw new Exception();
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingNode_Method method)
            {
                if (Index==0||method.Nodes.Length == 0) return;
                else new _Command_Load(Names,Index-1).GetInstruction(method.Nodes[0], current, next);
            }
            else if (node is SearchingNode_Variable variable)
            {
                new ILCode_LdLoc(current, variable.Variable);
                return;
            }
            else if (node is SearchingNode_Parameter parameter)
            {
                new ILCode_LdArg(current, parameter.Parameter);
                return;
            }
            else if(node is SearchingNode_Property property)
            {
                if(!property.Property.Getter.Attributes.Contains("static"))
                new _Command_Load(Names, Index - 1).GetInstruction(property.Nodes[0], current, next);
                new ILCode_Call(current, property.Property.Getter);
                return;
            }
            else throw new Exception();
        }

        public override string ToString() => string.Join(".", Names);
    }
}
