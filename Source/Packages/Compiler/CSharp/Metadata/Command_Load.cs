namespace Compiler.CSharp.Metadata
{
    public class Command_Load:ICommand
    {
        public string[] Names;
        public Command_Load(params string[] names)=>Names= names;
        public override string ToString() => string.Join(".", Names);
    }
}
