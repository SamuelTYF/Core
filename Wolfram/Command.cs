namespace Wolfram.NETLink
{
    public class Command: ICommand
    {
        public string Head;
        public object[] Values;
        public Command(string head,params object[] values)
        {
            Head = head;
            Values = values;
        }
        public void ToLink(IKernelLink link)
        {
            link.PutFunction(Head, Values.Length);
            foreach (object value in Values)
                if (value is ICommand)
                    (value as ICommand).ToLink(link);
                else link.Put(value);
        }
    }
}
