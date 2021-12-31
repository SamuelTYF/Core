using Collection;
namespace Parser
{
	public class JoinParseResult : IParseResult
	{
		public int Count;
		public List<IParseResult> TaskResults;
		public List<IParseResult> SperaterResults;
		public JoinParseResult(IParser parser,IStringArg s)
			: base(parser,s)
		{
			Count = 0;
			TaskResults = new();
			SperaterResults = new();
		}
        public override ParsedObject GetParsedObject(IStringArg s)
        {
			ParsedObject obj = new(Parser.TreeNode);
			for (int i=0;i<Count;i++)
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
