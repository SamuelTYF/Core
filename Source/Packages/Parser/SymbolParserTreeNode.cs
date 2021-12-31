using System;
using Collection;
using Net.Json;
namespace Parser
{
	public class SymbolParserTreeNode : IParserTreeNode
	{
		public string Key;
		public List<char> Characters;
		public SymbolParserTreeNode(string name, string key, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			Key = key;
			Characters = new(key.ToCharArray());
		}
		public override IParser Install()
		{
			if (Installed) return Parser;
			Installed = true;
			return Parser = new SymbolParser(Characters.ToArray())
			{
				TreeNode = this
			};
		}
        public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree) 
			=> new SymbolParserTreeNode(name,
				tree.GetNode($"{name}_key",node["key"]),
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
	}
}
