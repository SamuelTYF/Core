using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_And:ICommand
    {
        public ICommand A;
        public ICommand B;
        public Command_And(ICommand a, ICommand b)
        {
            A = a;
            B = b;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult a = A.Build(top).Condition(boolean);
            SearchingResult b = B.Build(top).Condition(boolean);
            if (a.Values.Count != 1 || b.Values.Count != 1) return new();
            else return new(new SearchingCondition(boolean, a.Values.Peek(), b.Values.Peek()));
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingCondition sc)
            {
                if (sc.TrueBlock)
                {
                    ILCode_Block block = new(null);
                    (sc.Nodes[1] as SearchingCondition).TrueBlock = true;
                    A.GetInstruction(sc.Nodes[0], current, block);
                    B.GetInstruction(sc.Nodes[1], current, next);
                    current.Codes.Add(block);
                    block.Parent = current;
                }
                else
                {
                    A.GetInstruction(sc.Nodes[0], current, next);
                    B.GetInstruction(sc.Nodes[1], current, next);
                }
            }
            else throw new Exception();
        }
        public override string ToString() => $"{A}&&{B}";
    }
}
