namespace Parser
{
	public class SpaceParseResult : IParseResult
	{
		public int Count;
        public SpaceParseResult(IParser parser, IStringArg s)
            : base(parser, s) => Count = 0;
		public override ParsedObject GetParsedObject(IStringArg s) => null;
	}
}
