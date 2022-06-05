using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Metadata
{
    public class Command_DoWhile : ICommand
    {
        public ICommand Condition;
        public ICommand[] Bodies;
        public Command_DoWhile(ICommand condition, ICommand[] bodies)
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
            ILCode_Block body = new(current);
            ILCode_Block condition = new(current);
            (sb.Nodes[0] as SearchingCondition).TrueBlock = true;
            Condition.GetInstruction(sb.Nodes[0], condition, body);
            for (int i = 0; i < Bodies.Length; i++)
                Bodies[i].GetInstruction(sb.Nodes[i + 1], body, next);
        }

        public override string ToString()
            => $"do{{\n\t{string.Join<ICommand>("\n\t", Bodies)}\n}}while({Condition});";
    }
}
