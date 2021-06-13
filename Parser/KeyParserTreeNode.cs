using System;
using Collection;
using Net.Json;
namespace Parser
{
	public class KeyParserTreeNode : IParserTreeNode
	{
		public string Key;
		public string TKey;
		public KeyParserTreeNode(string name, string key, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			Key = key;
			TKey = "";
			for (int i = 0; i < key.Length; i++)
				if (key[i] == '\\')
                    TKey += key[++i] switch
                    {
                        'n' => "\n",
                        'r' => "\r",
                        _ => throw new Exception(),
                    };
				else TKey += key[i];
		}
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
