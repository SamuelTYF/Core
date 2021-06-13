namespace Parser
{
	public class OptionParseResult : IParseResult
	{
		public string Value;
		public int Index;
		public int Count;
		public bool CanEmpty;
		public IParseResult[] OptionResults;
		public IParseResult[] TaskResults;
		public OptionParseResult(IParser parser, IStringArg s, int count, bool empty) 
			: base(parser, s)
		{
			Index = -1;
			Count = count;
			CanEmpty = empty;
			OptionResults = new IParseResult[count];
			TaskResults = new IParseResult[count + (CanEmpty ? 1 : 0)];
		}
		public override ParsedObject GetParsedObject(IStringArg s)
		{
			ParsedObject obj = new(Parser.TreeNode);
			if (Index != Count)
			{
				ParsedObject option = OptionResults[Index].GetParsedObject(s);
				if (option != null)
				{
					if (!option.Select) obj.Objects.AddList(option.Objects);
					else obj.Objects.Add(option);
				}
			}
			ParsedObject task = TaskResults[Index].GetParsedObject(s);
			if (task != null)
			{
				if (!task.Select) obj.Objects.AddList(task.Objects);
				else obj.Objects.Add(task);
			}
			if (obj.Select&&obj.Objects.Length == 0)
				obj.Value = s.Get(StartIndex, EndIndex - StartIndex);
			return obj;
		}
	}
}
