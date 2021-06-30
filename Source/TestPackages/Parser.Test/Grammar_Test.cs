using Parser;
using TestFramework;
namespace Parser_Test
{
    public class Grammar_Test:ITest
    {
        public Grammar_Test()
            : base("Grammar_Test",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            ParserTree tree = new();
            tree.FromJson(Parser.Properties.Resources.Parser);
            update(1);
            using VisualStringArg arg = new(Parser.Properties.Resources.Grammar_Parser);
            IParser parser= tree.Trees["Parsers"].Install();
            IParseResult result = parser.Parse(arg);
            Ensure.IsTrue(result.Success);
            Ensure.Equal(result.EndIndex, Parser.Properties.Resources.Grammar_Parser.Length);
            update(2);
            ParsedObject obj = result.GetParsedObject(arg);
            update(3);
            UpdateInfo(obj);
            Save(obj, "Grammar.txt");
            update(4);
            Save(GrammarParser.Print(tree.Trees), "Parser.txt");
        }
    }
}
