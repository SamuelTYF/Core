namespace Parser
{
	public class KeyParseResult : IParseResult
	{
		public string Key;
		public string Value;
		public int Count;
		public KeyParseResult(IParser parser,IStringArg s, string key)
			: base(parser,s)
		{
			Key = key;
			Count = 0;
			Value = "";
		}
		public override ParsedObject GetParsedObject(IStringArg arg)
		{
			ParsedObject obj = new(Parser.TreeNode);
			obj.Value = Value;
			return obj;
		}
	}
}
