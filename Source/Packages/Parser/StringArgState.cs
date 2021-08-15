namespace Parser
{
	public struct StringArgState
	{
		public int Index;
		public char This;
		public bool NotOver;
		public StringArgState(int index, char @this, bool notover)
		{
			Index = index;
			This = @this;
			NotOver = notover;
		}
	}
}
