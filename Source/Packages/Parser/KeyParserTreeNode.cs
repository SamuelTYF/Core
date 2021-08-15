using System;
using Collection;
using Net.Json;
namespace Parser
{
	public class KeyParserTreeNode : IParserTreeNode
	{
		public string Key;
        public KeyParserTreeNode(string name, string key, TrieTree<IParserTreeNode> tree)
            : base(name, tree) => Key = key;
        public override IParser Install()
		{
			if (Installed) return Parser;
			Installed = true;
			return Parser = new KeyParser(Key)
			{
				TreeNode = this
			};
		}
		public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree)
			=> new KeyParserTreeNode(name,
				(node["key"] as StringNode).Value,
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
	}
}
