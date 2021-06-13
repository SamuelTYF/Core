using System;
namespace Net.Json
{
	public class BooleanNode : Node
	{
		public bool Value;
		public override Node this[object key]
        {
            get => throw new Exception();
            set => throw new Exception();
        }
        public BooleanNode(bool value) => Value = value;
        public static implicit operator bool(BooleanNode node) => node.Value;
        public static implicit operator BooleanNode(bool str) => new(str);
        protected internal new static Node Parse(StringArg arg)
		{
			string text = null;
			int num = 0;
			bool value = true;
			int num2 = 0;
			while (arg.NotOver)
			{
				if (text == null)
				{
					if (arg.Top() == 't')
					{
						text = "true";
						num = 4;
					}
					else if (arg.Top() == 'f')
					{
						text = "false";
						num = 5;
						value = false;
					}
					else arg.Pop();
					continue;
				}
				if (text[num2] == arg.Top())
				{
					arg.Pop();
					num2++;
				}
				if (num2 != num)continue;
                return !arg.NotOver || arg.Top() is '\r' or '\n' or ' ' or ',' or ']' or '}'
                    ? new BooleanNode(value)
                    : throw new Exception();
            }
            throw new Exception();
		}
        public override string ToString() => !Value ? "false" : "true";
        public override string Print() => ToString();
    }
}
