using Collection;
using Net.Json;
namespace Parser
{
	public class StreamParserTreeNode : IParserTreeNode
	{
		public int Count;
		public string[] TaskNames;
		public IParserTreeNode[] Tasks;
		public InstallTreeNode InstallTreeNode;
		public StreamParserTreeNode(string name, string[] tasks, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			StreamParserTreeNode streamParserTreeNode = this;
			TaskNames = tasks;
			Count = tasks.Length;
			InstallTreeNode = delegate
			{
				streamParserTreeNode.Tasks = new IParserTreeNode[streamParserTreeNode.Count];
				for (int i = 0; i < streamParserTreeNode.Count; i++)
					streamParserTreeNode.Tasks[i] = tree[tasks[i]];
			};
		}
		public override IParser Install()
		{
			if (Installed)return Parser;
			Installed = true;
			InstallTreeNode();
			StreamParser streamParser = (StreamParser)(Parser = new StreamParser(Count));
			streamParser.TreeNode = this;
			for (int i = 0; i < Count; i++)
				streamParser.Tasks[i] = Tasks[i].Install();
			return Parser;
		}
		public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree)
		{
			ArrayNode arrayNode = node["tasks"] as ArrayNode;
			string[] array = new string[arrayNode.Nodes.Length];
			for (int i = 0; i < array.Length; i++)
				array[i] = tree.GetNode($"{name}_{i}", arrayNode[i]);
			return new StreamParserTreeNode(name, array, tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
		}
	}
}
