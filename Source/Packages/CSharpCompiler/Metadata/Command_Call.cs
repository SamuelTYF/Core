using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public class Command_Call:ICommand
    {
        public ICommand Func;
        public ICommand[] Params;
        public Command_Call(ICommand func, ICommand[] @params)
        {
            Func = func;
            Params = @params;
        }
        public override string ToString()
            => $"{Func}({string.Join<ICommand>(",", Params)})";
        public SearchingResult Build(SearchingResult top)
        {
            SearchingResult func = Func.Build(top);
            SearchingResult[] parameters = Params.Select(param => param.Build(top).Load()).Where(param => param.Values.Count > 0).ToArray();
            if (parameters.Length != Params.Length)return new();
            SearchingResult result =func.Call(parameters);
            if (result.Values.Count > 0) return result;
            return func.CallVirt(parameters);
        }
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next)
        {
            SearchingValue sv = node as SearchingValue;
            SearchingNode_Method method = sv.Nodes[0] as SearchingNode_Method;
            for (int i = 0; i < Params.Length; i++)
                Params[i].GetInstruction(sv.Nodes[i + 1] as SearchingValue, current, next);
            Func.GetInstruction(sv.Nodes[0], current, next);
            new ILCode_Call(current, method.Method);
        }
    }
}
