using Collection;
namespace Net.Http
{
	public class Header_URL
	{
		public List<string> Path;
		public TrieTree<string> Values;
        public Header_URL() => Values = new TrieTree<string>();
		public override string ToString() => $"{Path}\n{Values.ToString("\n")}";
	}
}
