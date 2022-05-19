using Compiler.CSharp.Searching;

namespace Compiler.CSharp.Metadata
{
    public interface ICommand
    {
        public SearchingResult Build(SearchingResult top);
    }
}
