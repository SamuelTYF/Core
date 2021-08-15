namespace Parser
{
	public class SymbolParseResult : IParseResult
	{
		public char[] Ends;
		public int Index;
		public char End;
        public SymbolParseResult(IParser parser, IStringArg s, char[] ends)
            : base(parser, s) => Ends = ends;
		public override ParsedObject GetParsedObject(IStringArg _)
		{
			ParsedObject obj = new(Parser.TreeNode);
			obj.Value = End;
			return obj;
		}
	}
}
