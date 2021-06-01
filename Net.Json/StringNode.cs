using System;
namespace Net.Json
{
	public class StringNode : Node
	{
		public string Value;
		public override Node this[object key]
        {
            get => throw new Exception();
            set => throw new Exception();
        }
        public StringNode(string value) => Value = value.ToTransferredString();
        public static implicit operator string(StringNode node) => node.Value;
        public static implicit operator StringNode(string str) => new(str);
        protected internal new static Node Parse(StringArg arg) => new StringNode(arg.GetString());
        public override string ToString() => "\"" + Value + "\"";
        public override string Print() => ToString();
    }
}
