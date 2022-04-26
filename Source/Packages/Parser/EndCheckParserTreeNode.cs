using System;
using Collection;
using Net.Json;
namespace Parser
{
	public class EndCheckParserTreeNode : IParserTreeNode
	{
		public EndCheckParserTreeNode(string name, TrieTree<IParserTreeNode> tree)
			: base(name, tree) { }
        public override IParser Install()
		{
			if (Installed) return Parser;
			Installed = true;
			return Parser = new EndCheckParser()
			{
				TreeNode = this
			};
		}
		public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree)
			=> new EndCheckParserTreeNode(name,
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
	}
}
