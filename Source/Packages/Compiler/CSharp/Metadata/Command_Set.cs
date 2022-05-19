namespace Compiler.CSharp.Metadata
{
    public class Command_Set:ICommand
    {
        public ICommand A;
        public ICommand B;
        public Command_Set(ICommand a, ICommand b)
        {
            A = a;
            B = b;
        }
        public override string ToString() => $"{A}={B}";
    }
}
