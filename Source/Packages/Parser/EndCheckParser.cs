namespace Parser
{
	public class EndCheckParser : IParser
	{
        public override IParseResult Parse(IStringArg s) => new EndCheckParseResult(this, s, !s.NotOver);
		public override string Print() => "";
		public override string ToString() => "";
	}
}
