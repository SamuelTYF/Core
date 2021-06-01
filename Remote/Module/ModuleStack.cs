using System;
namespace Remote.Module
{
	public class ModuleStack
	{
		public StackNode Top;
		public StackNode Bottom;
		public int Count;
        public ModuleStack() => Count = 0;
        public void Insert(ClientModule module)
		{
			Count++;
			StackNode stackNode = new(module);
			if (Bottom == null)
			{
				Top = Bottom = stackNode;
				return;
			}
			Bottom.Next = stackNode;
			Bottom = stackNode;
		}
		public ClientModule Pop()
		{
			if (Count == 0)
				throw new Exception();
			Count--;
			ClientModule value = Top.Value;
			Top = Top.Next;
			if (Count == 0)
				Bottom = null;
			return value;
		}
	}
}
