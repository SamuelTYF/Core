using CSharpCompiler.Code;
using CSharpCompiler.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Metadata
{
    public class Command_Operator2:ICommand
    {
        public ICommand A;
        public ICommand B;
        public string Operator;
        public Command_Operator2(ICommand a, ICommand b, string @operator)
        {
            A = a;
            B = b;
            Operator = @operator;
        }
        public SearchingResult Build(SearchingResult top)
        {
            SearchingResult a = A.Build(top).Load();
            SearchingResult b = B.Build(top).Load();
            if (a.Values.Count != 1 || b.Values.Count != 1) return new();
            else
            {
                SearchingValue avalue = a.Values.Peek() as SearchingValue;
                SearchingValue bvalue = b.Values.Peek() as SearchingValue;
                if (avalue.Type != bvalue.Type) return new();
                else return new(new SearchingValue(avalue.Type, avalue,bvalue));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingValue sv = node as SearchingValue;
            A.GetInstruction(sv.Nodes[0], current, next);
            B.GetInstruction(sv.Nodes[1], current, next);
            new ILCode_Operator2(current, Operator);
        }
        public override string ToString() => $"{A}{Operator}{B}";
    }
}
