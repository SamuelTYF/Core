namespace Parser
{
	public abstract class IParser
	{
		public IParserTreeNode TreeNode;
		public abstract IParseResult Parse(IStringArg s);
	}
}
