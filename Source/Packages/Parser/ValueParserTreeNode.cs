using System;
using Collection;
using Net.Json;
namespace Parser
{
	public class ValueParserTreeNode : IParserTreeNode
	{
		public string Key;
		public List<char> Characters;
		public bool Transferred;
		public ValueParserTreeNode(string name, string key, bool transferred, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			Key = key;
			Characters = new(key.ToCharArray());
			Transferred = transferred;
		}
		public override IParser Install()
		{
			if (Installed)return Parser;
			Installed = true;
			return Parser = new ValueParser(Transferred, Characters.ToArray())
			{
				TreeNode = this
			};
		}
        public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree) 
			=> new ValueParserTreeNode(name,
				(node["key"] as StringNode).Value,
				node["transferred"] == null || (node["transferred"] as BooleanNode).Value,
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
	}
}
