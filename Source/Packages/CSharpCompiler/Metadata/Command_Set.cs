using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_Set:ICommand
    {
        public ICommand A;
        public ICommand B;
        public Command_Set(ICommand a, ICommand b)
        {
            A = a;
            B = b;
        }
        public SearchingResult Build(SearchingResult top)
            => A.Build(top).Store(B.Build(top).Load());
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingValue sv = node as SearchingValue;
            ILCode_Block bblock = new(current);
            ILCode_Block ablock = new(current);
            B.GetInstruction(sv.Nodes[1], bblock, next);
            A.GetInstruction(sv.Nodes[0], ablock, next);
            if (sv.Nodes[0] is SearchingNode_Field field)
                new ILCode_StFld(current, field.Field);
            else if (sv.Nodes[0] is SearchingNode_Variable variable)
                new ILCode_StLoc(current, variable.Variable);
            else if (sv.Nodes[0] is SearchingNode_Parameter parameter)
                new ILCode_StArg(current, parameter.Parameter);
            else throw new Exception();
        }
        public override string ToString() => $"{A}={B}";
    }
}
