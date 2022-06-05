using CSharpCompiler.Code;
using CSharpCompiler.Searching;

namespace CSharpCompiler.Metadata
{
    public interface ICommand
    {
        public SearchingResult Build(SearchingResult top);
        public void GetInstruction(ISearchingObject node, ILCode_Block current, ILCode_Block next);
    }
}
