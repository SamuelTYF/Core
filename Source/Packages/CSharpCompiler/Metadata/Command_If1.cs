using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Metadata
{
    public class Command_If1 : ICommand
    {
        public ICommand Condition;
        public ICommand[] Trues;
        public Command_If1(ICommand condition, ICommand[] trues)
        {
            Condition = condition;
            Trues = trues;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult condition = Condition.Build(top).Condition(boolean);
            SearchingResult[] trues = Trues.Select(@true=>@true.Build(top).Load()).ToArray();
            if (condition.Values.Count != 1 || trues.Where(@true => @true.Values.Count != 1).Count() > 0) return new();
            else
            {
                List<ISearchingObject> nodes = new();
                nodes.Add(condition.Values.Peek());
                nodes.AddRange(trues.Select(@true => @true.Values.Peek()));
                return new(new SearchingBlock(nodes.ToArray()));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingValue sv = node as SearchingValue;
            ILCode_Block cblock = new(current);
            ILCode_Block tblock = new(current);
            Condition.GetInstruction(sv.Nodes[0], cblock, next);
            for(int i=0;i<Trues.Length;i++)
                Trues[i].GetInstruction(sv.Nodes[i+1], tblock, next);
        }

        public override string ToString()
            => $"if({Condition})\n{{\n\t{string.Join<ICommand>("\n\t",Trues)}\n}}";
    }
}
