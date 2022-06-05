using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace CSharpCompiler.Metadata
{
    public class Command_If2 : ICommand
    {
        public ICommand Condition;
        public ICommand[] Trues;
        public ICommand[] Falses;
        public Command_If2(ICommand condition, ICommand[] trues, ICommand[] falses)
        {
            Condition = condition;
            Trues = trues;
            Falses = falses;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult condition = Condition.Build(top).Condition(boolean);
            SearchingResult[] trues = Trues.Select(@true => @true.Build(top).Load()).ToArray();
            SearchingResult[] falses = Falses.Select(@false => @false.Build(top).Load()).ToArray();
            if (condition.Values.Count != 1 || trues.Where(@true => @true.Values.Count != 1).Count() > 0 || falses.Where(@false => @false.Values.Count != 1).Count() > 0) return new();
            else
            {
                List<ISearchingObject> nodes = new();
                nodes.Add(condition.Values.Peek());
                nodes.AddRange(trues.Select(@true => @true.Values.Peek()));
                nodes.AddRange(falses.Select(@false => @false.Values.Peek()));
                return new(new SearchingBlock(nodes.ToArray()));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingBlock sv = node as SearchingBlock;
            ILCode_Block cblock = new(current);
            ILCode_Block tblock = new(current);
            ILCode_Block fblock = new(current);
            Condition.GetInstruction(sv.Nodes[0], cblock, fblock);
            for (int i = 0; i < Trues.Length; i++)
                Trues[i].GetInstruction(sv.Nodes[i + 1], tblock, next);
            for (int i = 0; i < Falses.Length; i++)
                Falses[i].GetInstruction(sv.Nodes[i + 1+Trues.Length], fblock, next);
            new ILCode_Br(tblock, Br_Operator.br, next);
        }

        public override string ToString()
            => $"if({Condition})\n{{\n\t{string.Join<ICommand>("\n\t",Trues)}\n}}\nelse\n{{\n\t{string.Join<ICommand>("\n\t",Falses)}\n}}";
    }
}
