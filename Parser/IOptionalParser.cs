using System.Collections.Generic;
namespace Parser
{
	public abstract class IOptionalParser : IParser
	{
		public abstract IEnumerable<IParseResult> Parses(IStringArg s);
	}
}
