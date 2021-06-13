using Parser;
using TestFramework;
namespace Parser_Test
{
    public class Json_Test:ITest
    {
        public Json_Test()
            :base("Json_Test",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            ParserTree tree = new();
            tree.FromJson(Parser.Properties.Resources.Json);
            update(1);
            IParser parser=tree.Trees["dic"].Install();
            update(2);
            using VisualStringArg arg=new(Parser.Properties.Resources.Json);
            IParseResult result= parser.Parse(arg);
            Ensure.IsTrue(result.Success);
            update(3);
            ParsedObject obj = result.GetParsedObject(arg);
            update(4);
            UpdateInfo(obj);
            Save(obj, "json.txt");
        }
    }
}
