using Parser;
using TestFramework;
namespace Parser_Test
{
    public class CSharpGrammar_Test : ITest
    {
        public CSharpGrammar_Test()
            : base("CSharpGrammar_Test ", 3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            var tree = GrammarParser.GetTree(GrammarParser.GetParsedObject(Parser.Properties.Resources.CSharp));
            Save(GrammarParser.Print(tree), "CSharpGrammar.txt");
            update(1);
            using StringArg arg = new("using System;");
            IParser parser = tree["@using Assembly@"].Install();
            IParseResult result = parser.Parse(arg);
            Ensure.IsTrue(result.Success);
            Ensure.Equal(result.EndIndex, arg.Value.Length);
            update(2);
            ParsedObject obj = result.GetParsedObject(arg);
            Save(obj, "CSharp.txt");
            update(3);
        }
    }
}
