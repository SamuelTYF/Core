using Automata;
using Collection;
namespace Net.Http
{
	public class Pre_Header
	{
		public Header_URL URL;
		public string Mode;
		public string HTTP;
		public TrieTree<string> Values;
		public StringArg Data;
        public Pre_Header() => Values = new TrieTree<string>();
        public override string ToString() => $"{Mode}\n{URL}\n{Values.ToString("\n")}";
    }
}
