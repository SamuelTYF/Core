namespace Parser
{
	public class KeyParser : IParser
	{
		public string Key;
		public int Length;
        public KeyParser(string key) => Length = (Key = key).Length;
        public override IParseResult Parse(IStringArg s)
		{
			KeyParseResult keyParseResult = new(this,s, Key);
			int num = 0;
			while (s.NotOver && num < Length)
			{
				keyParseResult.Value += s.This;
				if (Key[num] == s.This)
				{
					num++;
					s.MoveToNext();
					continue;
				}
				break;
			}
			keyParseResult.Count = num;
			keyParseResult.Success = num == Length;
			keyParseResult.EndIndex = s.Index;
			return keyParseResult;
		}
	}
}
