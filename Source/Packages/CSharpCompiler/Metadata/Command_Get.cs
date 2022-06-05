using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_Get:ICommand
    {
        public ICommand A;
        public string Name;
        public Command_Get(ICommand a, string name)
        {
            A = a;
            Name = name;
        }
        public SearchingResult Build(SearchingResult top)
            => A.Build(top).Get(Name);

        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingNode_Method method)
            {
                A.GetInstruction(method.Nodes[0], current, next);
            }
            else throw new Exception();
        }

        public override string ToString() => $"{A}.{Name}";
    }
}
