namespace Compiler.CSharp.Metadata
{
    public class Command_Get:ICommand
    {
        public ICommand A;
        public string Name;
        public Command_Get(ICommand a, string name)
        {
            A = a;
            Name = name;
        }
        public override string ToString() => $"{A}.{Name}";
    }
}
