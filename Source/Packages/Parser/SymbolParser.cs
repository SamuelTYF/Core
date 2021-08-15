namespace Parser
{
	public class SymbolParser : IParser
	{
		public char[] Ends;
        public SymbolParser(params char[] ends) => Ends = ends;
        public override IParseResult Parse(IStringArg s)
        {
            SymbolParseResult symbolParseResult = new(this, s, Ends);
            if (s.NotOver)
                foreach (char end in Ends)
                    if (s.This == end)
                    {
                        symbolParseResult.End = end;
                        symbolParseResult.Success = true;
                        s.MoveToNext();
                        return symbolParseResult;
                    }
            return symbolParseResult;
        }
        public override string ToString()
        {
            string s = TreeNode.Main ? $"{TreeNode.Name}:" : "";
            string t = "";
            foreach (char end in Ends)
                if (end is >= 'a' and <= 'z') t += end;
                else t += "\\" + end;
            return Ends.Length == 1 ? s + t : $"{s}[{t}]";
        }
    }
}
