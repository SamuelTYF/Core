namespace Remote.Module
{
	public class LinkNode
	{
		public ServerModule Value;
		public LinkNode Last;
		public LinkNode Next;
        public LinkNode(ServerModule value) => Value = value;
        public void Empty() => Last = Next = null;
    }
}
