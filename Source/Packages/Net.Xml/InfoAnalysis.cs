using Collection;
using System.Text.RegularExpressions;

namespace Net.Xml
{
    public static class InfoAnalysis
    {
        public static readonly Regex Sign = new("([^\\s\\\"]*)=\"(.[^\"]*)\"",RegexOptions.Compiled);
        public static TrieTree<string> Run(string s)
        {
            TrieTree<string> R = new();
            foreach(Match m in Sign.Matches(s))
                R[m.Groups[1].Value]=m.Groups[2].Value;
            return R;
        }
    }
}
