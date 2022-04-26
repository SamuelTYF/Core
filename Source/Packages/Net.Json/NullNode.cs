using System;
namespace Net.Json
{
	public class NullNode : Node
	{
		private static readonly string key = "null";
		public override Node this[object key]
        {
            get => throw new Exception();
            set => throw new Exception();
        }
        protected internal new static Node Parse(StringArg arg)
		{
			int num = 0;
			while (arg.NotOver)
			{
				if (key[num] == arg.Top())
				{
					arg.Pop();
					num++;
				}
				if (num == 4)
				{
					while (arg.Top() is ' ' or '\t' or '\n' or '\r')
						arg.Pop();
					return !arg.NotOver || arg.Top() is ',' or ']' or '}'
                        ? new NullNode()
                        :throw new Exception();
                }
            }
			throw new Exception();
		}
        public override string ToString() => "null";
        public override string Print() => ToString();
		public override string FormatPrint(string suffix = "") => ToString();
    }
}
