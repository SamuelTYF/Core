namespace Parser
{
	public class NullParseResult : IParseResult
	{
        public NullParseResult(IParser parser, IStringArg s)
            : base(parser, s) => Success = true;
		public override ParsedObject GetParsedObject(IStringArg _) => null;
	}
}
