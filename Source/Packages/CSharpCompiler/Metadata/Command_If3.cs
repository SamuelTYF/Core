using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Metadata
{
    public class Command_If3:ICommand
    {
        public ICommand Condition;
        public ICommand True;
        public ICommand False;
        public Command_If3(ICommand condition, ICommand @true, ICommand @false)
        {
            Condition = condition;
            True = @true;
            False = @false;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult condition = Condition.Build(top).Condition(boolean);
            SearchingResult @true = True.Build(top).Load();
            SearchingResult @false = False.Build(top).Load();
            if (condition.Values.Count != 1 || @true.Values.Count != 1 || @false.Values.Count != 1) return new();
            else
            {
                SearchingValue truevalue = @true.Values.Peek() as SearchingValue;
                SearchingValue falsevalue = @false.Values.Peek() as SearchingValue;
                if (truevalue.Type != falsevalue.Type) return new();
                else return new(new SearchingValue(truevalue.Type,condition.Values.Peek(), truevalue, falsevalue));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingValue sv = node as SearchingValue;
            ILCode_Block cblock = new(current);
            ILCode_Block tblock = new(current);
            ILCode_Block fblock = new(current);
            Condition.GetInstruction(sv.Nodes[0], cblock, fblock);
            True.GetInstruction(sv.Nodes[1], tblock, next);
            False.GetInstruction(sv.Nodes[2], fblock, next);
            new ILCode_Br(tblock, Br_Operator.br, next);
        }

        public override string ToString()
            => $"{Condition}?{True}:{False}";
    }
}
