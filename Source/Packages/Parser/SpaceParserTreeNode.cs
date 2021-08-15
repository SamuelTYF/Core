using Collection;
using Net.Json;
namespace Parser
{
	public class SpaceParserTreeNode : IParserTreeNode
	{
		public int Min;
        public SpaceParserTreeNode(string name, int min, TrieTree<IParserTreeNode> tree)
            : base(name, tree) => Min = min;
		public override IParser Install()
		{
			if (Installed)return Parser;
			Installed = true;
			return Parser = new SpaceParser(Min)
			{
				TreeNode = this
			};
		}
        public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree) 
			=> new SpaceParserTreeNode(
				name,
				(int)(node["min"] as DoubleNode).Value,
				tree.Trees)
			{
				Select=node["select"]==null||(node["select"] as BooleanNode).Value
			};
    }
}
