using Collection;
using Net.Json;
namespace Parser
{
	public class NullParserTreeNode : IParserTreeNode
	{
		public NullParserTreeNode(string name, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
		}
		public override IParser Install()
		{
			if (Installed)return Parser;
			Installed = true;
			return Parser = new NullParser()
			{
				TreeNode = this
			};
		}
        public static IParserTreeNode FromJson(string name, ObjectNode _, ParserTree tree) 
			=> new NullParserTreeNode(name, tree.Trees);
    }
}
