namespace Parser
{
	public class StreamParseResult : IParseResult
	{
		public IParseResult[] TaskResults;
		public int Index;
		public int Count;
		public StreamParseResult(IParser parser,IStringArg s, int count)
			: base(parser,s)
		{
			Count = count;
			TaskResults = new IParseResult[count];
		}
		public override ParsedObject GetParsedObject(IStringArg s)
		{
			ParsedObject obj = new(Parser.TreeNode);
			for (int i = 0; i < Count; i++)
			{
				ParsedObject t = TaskResults[i].GetParsedObject(s);
				if (t != null)
				{
					if (!t.Select) obj.Objects.AddList(t.Objects);
					else obj.Objects.Add(t);
				}
			}
			if (obj.Select && obj.Objects.Length == 0)
				obj.Value = s.Get(StartIndex, EndIndex - StartIndex);
			return obj;
		}
	}
}
