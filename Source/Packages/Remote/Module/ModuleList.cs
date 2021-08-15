using System;
namespace Remote.Module
{
	public class ModuleList
	{
		public LinkNode Running;
		public LinkNode Free;
		public void Pop(LinkNode node)
		{
			if (Running == node)
			{
				Running = node.Next;
				if (Running != null)
					Running.Last = null;
			}
			else
			{
				if (node.Next != null)
					node.Next.Last = node.Last;
				node.Last.Next = node.Next;
			}
			node.Empty();
			node.Next = Free;
			Free = node;
		}
		public ServerModule Get()
		{
			if (Free == null)
				throw new Exception();
			LinkNode free = Free;
			Free = free.Next;
			free.Next = Running;
			if (Running != null)
				Running.Last = free;
			Running = free;
			return free.Value;
		}
		public void Insert(ServerModule value)
		{
			LinkNode node = new(value);
			value.Completed += delegate
			{
				Pop(node);
			};
			node.Next = Free;
			Free = node;
		}
		public void Disconnect()
		{
			while (Free != null)
			{
				Free.Value.Disconnect();
				Free = Free.Next;
			}
			while (Running != null)
			{
				Running.Value.Disconnect();
				Running = Running.Next;
			}
			while (Free != null)
			{
				Free.Value.Disconnect();
				Free = Free.Next;
			}
		}
	}
}
