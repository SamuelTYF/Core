using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_CompareOperator:ICommand
    {
        public ICommand A;
        public ICommand B;
        public string Operator;
        //Reverse
        public static readonly Dictionary<string, Br_Operator> TMaps;
        public static readonly Dictionary<string, Br_Operator> FMaps;
        static Command_CompareOperator()
        {
            TMaps = new();
            TMaps["<"] = Br_Operator.blt;
            TMaps[">"] = Br_Operator.bgt;
            TMaps["=="] = Br_Operator.beq;
            TMaps["!="] = Br_Operator.bne;
            FMaps = new();
            FMaps["<"] = Br_Operator.bge;
            FMaps[">"] = Br_Operator.ble;
            FMaps["=="] = Br_Operator.bne;
            FMaps["!="] = Br_Operator.beq;
        }
        public Command_CompareOperator(ICommand a, ICommand b, string @operator)
        {
            A = a;
            B = b;
            Operator = @operator;
        }
        public SearchingResult Build(SearchingResult top)
        {
            IType boolean = top.GetType("System", "Boolean");
            SearchingResult a = A.Build(top).Load();
            SearchingResult b = B.Build(top).Load();
            if (a.Values.Count != 1 || b.Values.Count != 1) return new();
            else
            {
                SearchingValue avalue = a.Values.Peek() as SearchingValue;
                SearchingValue bvalue = b.Values.Peek() as SearchingValue;
                if (avalue.Type != bvalue.Type) return new();
                else return new(new SearchingCondition(boolean,avalue, bvalue));
            }
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            if (node is SearchingValue sv)
            {
                throw new Exception();
                A.GetInstruction(sv.Nodes[0], current, next);
                B.GetInstruction(sv.Nodes[1], current, next);
                new ILCode_Operator2(current, Operator);
            }
            else if (node is SearchingCondition sc)
            {
                A.GetInstruction(sc.Nodes[0], current, next);
                B.GetInstruction(sc.Nodes[1], current, next);
                if (sc.TrueBlock)new ILCode_Br(current, TMaps[Operator], next);
                else new ILCode_Br(current, FMaps[Operator], next);
            }
            else throw new Exception();
        }
        public override string ToString() => $"{A}{Operator}{B}";
    }
}
