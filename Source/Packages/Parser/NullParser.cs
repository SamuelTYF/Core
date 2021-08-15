namespace Parser
{
	public class NullParser : IParser
	{
        public override IParseResult Parse(IStringArg s) => new NullParseResult(this, s);
		public override string Print() => "";
		public override string ToString() => "";
	}
}
