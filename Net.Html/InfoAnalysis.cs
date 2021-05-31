using System.Text.RegularExpressions;
using Collection;

namespace Net.Html
{
	public static class InfoAnalysis
	{
		public static readonly Regex Sign = new Regex("([^\\s]*)=\"(.[^\"]*)\"", RegexOptions.Compiled);
		public static TrieTree<string> Run(string s)
		{
			s = s.Replace('\'', '"');
			TrieTree<string> trieTree = new TrieTree<string>();
			foreach(Match m in Sign.Matches(s))
				trieTree[m.Groups[1].Value, 0] = m.Groups[2].Value;
			return trieTree;
		}
	}
}
