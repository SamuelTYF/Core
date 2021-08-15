using Collection;
using Net.Json;
namespace Parser
{
	public class OptionParserTreeNode : IParserTreeNode
	{
		public int Count;
		public string[] OptionNames;
		public string TaskName;
		public IParserTreeNode[] Options;
		public IParserTreeNode Task;
		public bool CanEmpty;
		public InstallTreeNode InstallTreeNode;
		public OptionParserTreeNode(string name, string[] options, string task, bool empty, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			OptionParserTreeNode optionalParserTreeNode = this;
			OptionNames = options;
			TaskName = task;
			CanEmpty = empty;
			Count = options.Length;
			InstallTreeNode = delegate
			{
				optionalParserTreeNode.Task = tree[optionalParserTreeNode.TaskName];
				optionalParserTreeNode.Options = new IParserTreeNode[optionalParserTreeNode.Count];
				for (int i = 0; i < optionalParserTreeNode.Count; i++)
				{
					optionalParserTreeNode.Options[i] = tree[optionalParserTreeNode.OptionNames[i]];
				}
			};
		}
		public override IParser Install()
		{
			if (Installed) return Parser;
			Installed = true;
			InstallTreeNode();
			OptionParser optionParser = (OptionParser)(Parser = new OptionParser(CanEmpty, Count));
			optionParser.TreeNode = this;
			optionParser.Task = Task.Install();
			for (int i = 0; i < Count; i++)
				optionParser.Options[i] = Options[i].Install();
			return Parser;
		}
		public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree)
		{
			ArrayNode arrayNode = node["options"] as ArrayNode;
			string[] array = new string[arrayNode.Nodes.Length];
			for (int i = 0; i < array.Length; i++)
				array[i] = tree.GetNode($"{name}[{i}]", arrayNode[i]);
			return new OptionParserTreeNode(
				name,
				array,
				node["task"] == null ? "null" : tree.GetNode($"{name}.task", node["task"]),
				(node["empty"] as BooleanNode).Value,
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
		}
	}
}
