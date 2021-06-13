using System.Collections.Generic;
namespace Parser
{
	public static class ParserTool
	{
		public static IEnumerable<IParseResult> GetEnumerator(this IParser p, IStringArg s)
		{
			if (p is IOptionalParser)
			{
				foreach (IParseResult item in (p as IOptionalParser).Parses(s))
				{
					yield return item;
				}
			}
			else
				yield return p.Parse(s);
		}
	}
}
