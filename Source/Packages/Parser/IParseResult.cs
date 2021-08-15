namespace Parser
{
	public abstract class IParseResult
	{
		public IParser Parser;
		public bool Success;
		public int StartIndex;
		public int EndIndex;
		public StringArgState State;
		public IParseResult(IParser parser,IStringArg s)
		{
			Parser = parser;
			StartIndex = s.Index;
			State = s.State;
		}
		public abstract ParsedObject GetParsedObject(IStringArg s);
	}
}
