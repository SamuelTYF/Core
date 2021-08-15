using Collection;
using Net.Json;
namespace Parser
{
	public abstract class IParserTreeNode
	{
		public bool Main;
		public bool Select;
		public IParser Parser;
		public bool Installed;
		public List<IParserTreeNode> Nodes;
		public string Name;
		public IParserTreeNode(string name, TrieTree<IParserTreeNode> tree)
		{
			Main = true;
			Select = true;
			Installed = false;
			Nodes = new();
			Name = name;
			tree[name] = this;
		}
		public abstract IParser Install();
	}
}
