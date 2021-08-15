using Collection;

namespace Parser
{
    public delegate string FromParsedObject(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree);
}
