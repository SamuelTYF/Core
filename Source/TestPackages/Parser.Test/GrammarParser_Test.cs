using Parser;
using TestFramework;
namespace Parser_Test
{
    public class GrammaParser_Test:ITest
    {
        public GrammaParser_Test()
            : base("GrammaParser_Test", 3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            var tree = GrammarParser.GetTree(GrammarParser.GetParsedObject(Parser.Properties.Resources.Grammar_Parser));
            Save(GrammarParser.Print(tree), "GrammarParser.txt");
            update(1);
            using StringArg arg = new(Parser.Properties.Resources.RE_Simple);
            IParser parser = tree["@Parsers@"].Install();
            IParseResult result = parser.Parse(arg);
            Ensure.IsTrue(result.Success);
            Ensure.Equal(result.EndIndex, Parser.Properties.Resources.RE_Simple.Length);
            update(2);
            ParsedObject obj = result.GetParsedObject(arg);
            update(3);
        }
    }
}
