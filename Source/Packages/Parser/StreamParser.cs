namespace Parser
{
	public class StreamParser : IParser
	{
		public IParser[] Tasks;
		public int Count;
        public StreamParser(int count) => Tasks = new IParser[Count = count];
        public StreamParser(params IParser[] tasks) => Count = (Tasks = tasks).Length;
        public IParseResult ParseOptional(IStringArg s, IOptionalParser ot, StreamParseResult r, int index)
		{
			foreach (IParseResult item in ot.Parses(s))
			{
				if (item.Success)
				{
					bool flag = true;
					for (int i = index + 1; i < Count; i++)
					{
						IParser parser = Tasks[i];
						if (parser is IOptionalParser)
						{
							flag = (r.TaskResults[i] = ParseOptional(s, parser as IOptionalParser, r, i))?.Success ?? true;
							break;
						}
						if (!(r.TaskResults[i] = parser.Parse(s)).Success)
						{
							r.Index = i;
							flag = false;
							break;
						}
					}
					if (flag)
					{
						r.Success = true;
						r.Index = Count;
						r.EndIndex = s.Index;
						return item;
					}
					s.Restore(item.State);
					continue;
				}
				return item;
			}
			return null;
		}
		public override IParseResult Parse(IStringArg s)
		{
			StreamParseResult streamParseResult = new(this,s, Count);
			for (int i = 0; i < Count; i++)
			{
				IParser parser = Tasks[i];
				if (parser is IOptionalParser)
				{
					streamParseResult.TaskResults[i] = ParseOptional(s, parser as IOptionalParser, streamParseResult, i);
					return streamParseResult;
				}
				if (!(streamParseResult.TaskResults[i] = parser.Parse(s)).Success)
				{
					streamParseResult.Index = i;
					return streamParseResult;
				}
			}
			streamParseResult.Success = true;
			streamParseResult.Index = Count;
			streamParseResult.EndIndex = s.Index;
			return streamParseResult;
		}
		public override string ToString()
		{
			string s = TreeNode.Main ? $"{TreeNode.Name}:" : "";
			string t = "";
			foreach (IParser task in Tasks)
				t += task.Print();
			return $"{s}({t})";
		}
	}
}
