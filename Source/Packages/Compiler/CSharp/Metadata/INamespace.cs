namespace Compiler.CSharp.Metadata
{
    public interface INamespace
    {
        public INamespace GetNamespace(string name);
        public IType GetType(string name);
    }
}
