using System;
using Collection;
using Net.Json;
namespace Parser
{
	/// <summary>
	/// @name@
	/// ~[] Value
	/// (|) Option
	/// [] Symbol.Group
	/// </summary>
	public class ParserTree
	{
		public TrieTree<FromJson> FromJsons;
		public TrieTree<IParserTreeNode> Trees;
		public ParserTree()
		{
			Trees = new TrieTree<IParserTreeNode>();
			FromJsons = new TrieTree<FromJson>();
			FromJsons["Space"] = SpaceParserTreeNode.FromJson;
			FromJsons["Value"] = ValueParserTreeNode.FromJson;
			FromJsons["Symbol"] = SymbolParserTreeNode.FromJson;
			FromJsons["Key"] = KeyParserTreeNode.FromJson;
			FromJsons["Stream"] = StreamParserTreeNode.FromJson;
			FromJsons["Option"] = OptionParserTreeNode.FromJson;
			FromJsons["Join"] = JoinParserTreeNode.FromJson;
			NullParserTreeNode.FromJson("null", null, this);
		}
		public void FromJson(ObjectNode node)
		{
			if (node == null)
				throw new Exception();
			node.Dictionary.Foreach(delegate(string name, TrieTree<Node> value)
			{
				ObjectNode objectNode = value.Value as ObjectNode;
				FromJsons[(objectNode["type"] as StringNode).Value](name, objectNode, this);
				Trees[name].Main = true;
			});
		}
        public void FromJson(string text) => FromJson(Node.Parse(text) as ObjectNode);
        public string GetNode(string name,Node node)
        {
			if (node is ObjectNode obj)
			{
				FromJsons[(obj["type"] as StringNode).Value](name, obj, this);
				Trees[name].Main = false;
				return name;
			}
			else return (node as StringNode).Value;
        }
	}
}
