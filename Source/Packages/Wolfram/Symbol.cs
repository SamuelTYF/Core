namespace Wolfram.NETLink
{
    public class Symbol:ICommand
    {
        public string Name;
        public Symbol(string name) => Name = name;
        public void ToLink(IKernelLink link) => link.PutSymbol(Name);
    }
}
