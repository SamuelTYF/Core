using Collection;
using Net.Json;
namespace Parser
{
	public delegate IParserTreeNode FromJson(string name, ObjectNode node, ParserTree tree);
}
