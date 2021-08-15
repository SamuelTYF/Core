using System;
namespace Parser
{
	public abstract class IStringArg:IDisposable
	{
		public StringArgState State;
		public int Index => State.Index;
		public char This => State.This;
		public bool NotOver => State.NotOver;
        public abstract void MoveToNext();
		public void Restore(StringArgState state) => State = state;
		public abstract void Dispose();
		public abstract string Get(int index, int length);
	}
}
