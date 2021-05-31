using Collection;
using System.Text.RegularExpressions;
namespace Net.Html
{
    public static class InfoAnalysis
    {
        public static readonly Regex Sign = new("([^\\s]*)=\"(.[^\"]*)\"", RegexOptions.Compiled);
        public static TrieTree<string> Run(string s)
        {
            s = s.Replace('\'', '"');
            TrieTree<string> trieTree = new();
            foreach (Match m in Sign.Matches(s))
                trieTree[m.Groups[1].Value, 0] = m.Groups[2].Value;
            return trieTree;
        }
    }
}
