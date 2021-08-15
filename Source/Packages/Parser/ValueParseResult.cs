namespace Parser
{
	public class ValueParseResult : IParseResult
	{
		public char[] Ends;
		public string Value;
		public char End;
        public ValueParseResult(IParser parser, IStringArg s, char[] ends)
            : base(parser, s) => Ends = ends;
		public override ParsedObject GetParsedObject(IStringArg _)
		{
			ParsedObject obj = new(Parser.TreeNode);
			obj.Value = Value;
			return obj;
		}
	}
}
