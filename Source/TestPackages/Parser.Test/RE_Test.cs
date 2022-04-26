using Parser;
using TestFramework;
namespace Parser_Test
{
    public class RE_Test:ITest
    {
        public RE_Test()
            : base("RE_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            var tree = GrammarParser.GetTree(GrammarParser.GetParsedObject(Parser.Properties.Resources.RE_Simple));
            Save(GrammarParser.Print(tree), "RE.txt");
            update(1);
            string text = "11*";
            using VisualStringArg arg = new(text);
            IParser parser = tree["@RE@"].Install();
            IParseResult result = parser.Parse(arg);
            Ensure.IsTrue(result.Success);
            Ensure.Equal(result.EndIndex, text.Length);
            update(2);
            ParsedObject obj = result.GetParsedObject(arg);
            update(3);
        }
    }
}
