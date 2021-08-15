using System;
using Collection;
namespace Net.Json
{
	public class ObjectNode : Node
	{
		public TrieTree<Node> Dictionary;
		public override Node this[object key]
        {
            get => key is not string ? throw new Exception() : Dictionary[(string)key, 0];
            set
            {
                if (key is not string)
                    throw new Exception();
                Dictionary[(string)key, 0] = value;
            }
        }
        public ObjectNode() => Dictionary = new TrieTree<Node>();
        public ObjectNode(List<(string, Node)> Nodes)
		{
			Dictionary = new TrieTree<Node>();
			foreach (var (key, value) in Nodes)
				Dictionary[key, 0] = value;
		}
		protected internal new static Node Parse(StringArg arg)
		{
			List<(string, Node)> list = new();
			arg.Pop();
			while (arg.NotOver)
				switch (arg.Top())
				{
					case '\t':
					case '\n':
					case '\r':
					case ' ':
						arg.Pop();
						break;
					case '}':
						arg.Pop();
						return new ObjectNode(list);
					case ',':
						arg.Pop();
						break;
					case '"':
						{
							string @string = arg.GetString();
							arg.GoTo(':');
							arg.Pop();
							list.Add((@string, Node.Parse(arg)));
							break;
						}
				}
			throw new Exception();
		}
        public override string ToString() => "{" + Dictionary.ToString() + "}";
        public override string Print()
		{
			string s = "";
			Dictionary.Foreach((key, node) => s = s == "" ? "\"" + key + "\":" + node.Value.Print() : s + ",\"" + key + "\":" + node.Value.Print());
			return "{" + s + "}";
		}
        public void Set(string key, Node value) => this[key] = value;
		public override string FormatPrint(string suffix = "")
        {
			List<string> ls = new();
			Dictionary.Foreach((name, node) => ls.Add($"{suffix}\t\"{name}\":{node.Value.FormatPrint(suffix + "\t")}"));
			return $"{{\n{string.Join(",\n", ls)}\n{suffix}}}";
        }
	}
}
