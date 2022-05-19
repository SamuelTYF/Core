namespace Compiler.CSharp.Metadata
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
    }
}
