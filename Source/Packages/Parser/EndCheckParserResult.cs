namespace Parser
{
	public class EndCheckParseResult : IParseResult
	{
		public EndCheckParseResult(IParser parser, IStringArg s, bool success)
			: base(parser, s) => Success = success;
		public override ParsedObject GetParsedObject(IStringArg _) => null;
	}
}
