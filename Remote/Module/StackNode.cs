namespace Remote.Module
{
	public class StackNode
	{
		public ClientModule Value;
		public StackNode Next;
        public StackNode(ClientModule value) => Value = value;
    }
}
