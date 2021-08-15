using System;
namespace Net.Json
{
	public abstract class Node
	{
		public abstract Node this[object key] { get; set; }
        public static Node Parse(string s) => Parse(new StringArg(s));
		public static Node Parse(string s, int index, int end)
			=> Parse(new StringArg(s)
			{
				index = index,
				end = end < 0 ? end + s.Length : end
			});
		protected internal static Node Parse(StringArg str)
		{
			while (str.NotOver)
			{
				switch (str.Top())
				{
				case '-':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return DoubleNode.Parse(str);
				case 'n':
					return NullNode.Parse(str);
				case '[':
					return ArrayNode.Parse(str);
				case '{':
					return ObjectNode.Parse(str);
				case '"':
					return StringNode.Parse(str);
				case ']':
				case '}':
					return null;
				case 'f':
				case 't':
					return BooleanNode.Parse(str);
				default:
					throw new Exception();
				case '\t':
				case '\n':
				case '\r':
				case ' ':
				case ',':
					break;
				}
				str.Pop();
			}
			throw new Exception();
		}
		public abstract string Print();
		public static implicit operator Node(string s)
		{
			try
			{
				return Parse(s);
			}
			catch (Exception)
			{
				return new StringNode(s);
			}
		}
		public abstract string FormatPrint(string suffix = "");
	}
}
