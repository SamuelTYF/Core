namespace Parser
{
	public class SpaceParser : IParser
	{
		public int Min;
		public SpaceParser(int min) => Min = min;
		public override IParseResult Parse(IStringArg s)
		{
			SpaceParseResult spaceParseResult = new(this,s);
			while (s.NotOver && (s.This is ' ' or '\n' or '\r' or '\t'))
			{
				spaceParseResult.Count++;
				s.MoveToNext();
			}
			spaceParseResult.Success = spaceParseResult.Count >= Min;
			spaceParseResult.EndIndex = s.Index;
			return spaceParseResult;
		}
	}
}
