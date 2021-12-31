using System;
using Collection;

namespace Net.Json
{
	public class ArrayNode : Node
	{
		public Node[] Nodes;
		public override Node this[object key]
		{
			get
			{
				if (key is not int)
					throw new Exception();
				int num = (int)key;
                return num < 0 || num > Nodes.Length ? throw new Exception() : Nodes[num];
            }
            set
			{
				if (key is not int)
					throw new Exception();
				int num = (int)key;
				if (num < 0 || num > Nodes.Length)
					throw new Exception();
				Nodes[num] = value;
			}
		}
        public ArrayNode(Node[] nodes) => Nodes = nodes;
        protected internal new static Node Parse(StringArg arg)
		{
			arg.Pop();
			List<Node> list = new();
			while (arg.NotOver)
			{
				Node node = Node.Parse(arg);
				if (node == null)
					break;
				list.Add(node);
			}
			if (arg.Top() != ']')
				throw new Exception();
			arg.Pop();
			return new ArrayNode(list.ToArray());
		}
		public override string ToString()
		{
			if (Nodes.Length == 0)
				return "[]";
			string text = "[" + Nodes[0].ToString();
			for (int i = 1; i < Nodes.Length; i++)
			{
				text = text + "," + Nodes[i].ToString();
			}
			return text + "]";
		}
		public override string Print()
		{
			if (Nodes.Length == 0)
				return "[]";
			string text = "[" + Nodes[0].Print();
			for (int i = 1; i < Nodes.Length; i++)
			{
				text = text + "," + Nodes[i].Print();
			}
			return text + "]";
		}
		public override string FormatPrint(string suffix = "")
		{
			List<string> ls = new();
			foreach(Node node in Nodes)
				ls.Add($"{suffix}\t{node.FormatPrint(suffix + "\t")}");
			return $"[\n{string.Join(",\n", ls)}\n{suffix}]";
		}
	}
}
