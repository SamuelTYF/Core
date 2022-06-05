using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_While : ICommand
    {
        public ICommand Condition;
        public ICommand[] Bodies;
        public Command_While(ICommand condition, ICommand[] bodies)
        {
            Condition = condition;
            Bodies = bodies;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult condition = Condition.Build(top).Condition(boolean);
            SearchingResult[] bodies = Bodies.Select(body => body.Build(top).Load()).ToArray();
            if (condition.Values.Count != 1 || bodies.Where(body => body.Values.Count != 1).Count() > 0) return new();
            else
            {
                List<ISearchingObject> values = new();
                values.Add(condition.Values.Peek());
                values.AddRange(bodies.Select(body => body.Values.Peek()));
                return new(new SearchingBlock(values.ToArray()));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingBlock sb = node as SearchingBlock;
            ILCode_Block condition = new(current);
            ILCode_Block body = new(current);
            Condition.GetInstruction(sb.Nodes[0], condition, next);
            for (int i = 0; i < Bodies.Length; i++)
                Bodies[i].GetInstruction(sb.Nodes[i + 1], body, next);
            new ILCode_Br(body, Br_Operator.br, condition);
        }
        public override string ToString()
            => $"while({Condition})\n{{\n\t{string.Join<ICommand>("\n\t", Bodies)}\n}}";
    }
}
