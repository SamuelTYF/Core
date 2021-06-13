using System;
namespace Parser
{
	public class ValueParser : IParser
	{
		public char[] Ends;
		public bool Transferred;
		public ValueParser(bool transferred, params char[] ends)
		{
			Transferred = transferred;
			Ends = ends;
		}
		public override IParseResult Parse(IStringArg s)
		{
			ValueParseResult valueParseResult = new(this,s, Ends);
			valueParseResult.Value = "";
			if (Transferred)
			{
				bool flag = false;
				while (s.NotOver)
				{
					if (flag)
					{
                        valueParseResult.Value += s.This switch
                        {
                            'r' => "\r",
                            'n' => "\n",
                            't' => "\t",
                            '"' => "\"",
                            _ => throw new Exception(),
                        };
                        flag = false;
					}
					else
					{
						foreach (char end in Ends)
							if (s.This == end)
							{
								valueParseResult.End = end;
								valueParseResult.Success = true;
								valueParseResult.EndIndex = s.Index;
								return valueParseResult;
							}
						if (s.This == '\\') flag = true;
						else valueParseResult.Value += s.This;
					}
					s.MoveToNext();
				}
			}
			else
				while (s.NotOver)
				{
					foreach (char end in Ends)
						if (s.This == end)
						{
							valueParseResult.End = end;
							valueParseResult.Success = true;
							valueParseResult.EndIndex = s.Index;
							return valueParseResult;
						}
					valueParseResult.Value += s.This;
					s.MoveToNext();
				}
			return valueParseResult;
		}
	}
}
