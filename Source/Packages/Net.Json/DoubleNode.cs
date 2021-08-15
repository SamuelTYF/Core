using System;
namespace Net.Json
{
	public class DoubleNode : Node
	{
		public double Value;
		public override Node this[object key]
        {
            get => throw new Exception();
            set => throw new Exception();
        }
        public DoubleNode(double value) => Value = value;
        public static implicit operator double(DoubleNode node) => node.Value;
        public static implicit operator DoubleNode(double str) => new(str);
        protected internal new static Node Parse(StringArg arg) => new DoubleNode(Convert.ToDouble(arg.EndWith(' ', '}', ']', ',')));
        public override string ToString() => Value.ToString();
        public override string Print() => ToString();
        public override string FormatPrint(string suffix = "") => ToString();
    }
}
