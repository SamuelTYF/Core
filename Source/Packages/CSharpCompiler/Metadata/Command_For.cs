using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Metadata
{
    public class Command_For : ICommand
    {
        public ICommand Init;
        public ICommand Condition;
        public ICommand Update;
        public ICommand[] Bodies;
        public Command_For(ICommand init,ICommand condition,ICommand update, ICommand[] bodies)
        {
            Init = init;
            Condition = condition;
            Update = update;
            Bodies = bodies;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult init = Init.Build(top).Load();
            SearchingResult condition = Condition.Build(top).Condition(boolean);
            SearchingResult update = Update.Build(top).Load();
            SearchingResult[] bodies = Bodies.Select(body => body.Build(top).Load()).ToArray();
            if (init.Values.Count != 1 || condition.Values.Count != 1 || update.Values.Count != 1 || bodies.Where(body => body.Values.Count != 1).Count() > 0) return new();
            else
            {
                List<ISearchingObject> values = new();
                values.Add(init.Values.Peek());
                values.Add(condition.Values.Peek());
                values.Add(update.Values.Peek());
                values.AddRange(bodies.Select(body => body.Values.Peek()));
                return new(new SearchingBlock(values.ToArray()));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingBlock sb = node as SearchingBlock;
            Init.GetInstruction(sb.Nodes[0], current, next);
            ILCode_Block condition = new(current);
            ILCode_Block body = new(current);
            Condition.GetInstruction(sb.Nodes[1], condition, next);
            for (int i = 0; i < Bodies.Length; i++)
                Bodies[i].GetInstruction(sb.Nodes[i + 3], body, next);
            Update.GetInstruction(sb.Nodes[2], body, next);
            new ILCode_Br(body, Br_Operator.br, condition);
        }

        public override string ToString()
            => $"for({Init};{Condition};{Update})\n{{\n\t{string.Join<ICommand>("\n\t", Bodies)}\n}}";
    }
}
