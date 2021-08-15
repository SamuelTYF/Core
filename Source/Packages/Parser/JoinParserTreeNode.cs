using Collection;
using Net.Json;
namespace Parser
{
	public class JoinParserTreeNode : IParserTreeNode
	{
		public string TaskName;
		public string SperaterName;
		public IParserTreeNode Task;
		public IParserTreeNode Sperater;
		public int Min;
		public int Max;
		public InstallTreeNode InstallTreeNode;
		public JoinParserTreeNode(string name, string sperater, string task, int min, int max, TrieTree<IParserTreeNode> tree)
			: base(name, tree)
		{
			JoinParserTreeNode joinParserTreeNode = this;
			SperaterName = sperater;
			TaskName = task;
			Min = min;
			Max = max;
			InstallTreeNode = delegate
			{
				joinParserTreeNode.Sperater = tree[sperater];
				joinParserTreeNode.Task = tree[task];
			};
		}
		public override IParser Install()
		{
			if (Installed) return Parser;
			Installed = true;
			InstallTreeNode();
			JoinParser joinParser = (JoinParser)(Parser = new JoinParser(Min, Max));
			joinParser.TreeNode = this;
			joinParser.Task = Task.Install();
			joinParser.Sperater = Sperater.Install();
			joinParser.IsOptional = joinParser.Task is IOptionalParser || joinParser.Sperater is IOptionalParser;
			return Parser;
		}
        public static IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree) 
			=> new JoinParserTreeNode(
				name,
				node["sperater"]==null?"null":tree.GetNode($"{name}_sperater",node["sperater"]),
				tree.GetNode($"{name}_task", node["task"]),
				(node["min"] != null) ? ((int)(node["min"] as DoubleNode).Value) : 0, 
				(node["max"] == null) ? int.MaxValue : ((int)(node["max"] as DoubleNode).Value), 
				tree.Trees)
			{
				Select = node["select"] == null || (node["select"] as BooleanNode).Value
			};
	}
}
