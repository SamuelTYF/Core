using Collection;

namespace Parser
{
    public class GrammarParser
    {
        public static readonly IParser Parser;
        public static TrieTree<FromParsedObject> FromParsedObjects;
        static GrammarParser()
        {
            ParserTree tree = new();
            tree.FromJson(Properties.Resources.Parser);
            Parser = tree.Trees["Parsers"].Install();
            FromParsedObjects = new();
            FromParsedObjects["Name"] = Name;
            FromParsedObjects["Space"] = Space;
            FromParsedObjects["ZeroSpace"] = ZeroSpace;
            FromParsedObjects["Symbol.Special"] = Symbol;
            FromParsedObjects["Symbol"] = Symbol;
            FromParsedObjects["Symbol.Group"] = SymbolGroup;
            FromParsedObjects["Value"] = Value;
            FromParsedObjects["Option"] = Option;
            FromParsedObjects["Join.1"] = Join1;
            FromParsedObjects["Join.2"] = Join2;
            FromParsedObjects["Join"] = Join;
            FromParsedObjects["Key"] = Key;
            FromParsedObjects["Stream"] = Stream;
            FromParsedObjects["EndCheck"] = EndCheck;
        }
        public static ParsedObject GetParsedObject(string grammar)
        {
            using StringArg arg = new(grammar);
            IParseResult result = Parser.Parse(arg);
            return result.Success ? result.GetParsedObject(arg) : null;
        }
        public static ParsedObject GetParsedObject(IStringArg arg)
        {
            IParseResult result = Parser.Parse(arg);
            return result.Success ? result.GetParsedObject(arg) : null;
        }
        public static TrieTree<IParserTreeNode> GetTree(ParsedObject obj)
        {
            TrieTree<IParserTreeNode> tree = new();
            new NullParserTreeNode("null", tree);
            if (obj.Name != "Parsers") throw new System.Exception();
            foreach (ParsedObject p in obj.Objects)
            {
                string s = (string)p.Objects[0].Value;
                if (p.Objects.Length == 2)
                {
                    s=Parse(s, p.Objects[1], tree);
                    tree[s].Main = true;
                }
                else
                {
                    string[] tasks = new string[p.Objects.Length - 1];
                    for (int i = 1; i < p.Objects.Length; i++)
                        tasks[i - 1] = Parse($"{s}[{i - 1}]", p.Objects[i], tree);
                    new StreamParserTreeNode(s, tasks, tree);
                }
            }
            return tree;
        }
        public static string Parse(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
            => FromParsedObjects[obj.Name](name, obj, tree);
        public static string Name(string _, ParsedObject obj, TrieTree<IParserTreeNode> __)=> (string)obj.Value;
        public static string Space(string name, ParsedObject _, TrieTree<IParserTreeNode> tree)
        {
            new SpaceParserTreeNode(name, 1, tree).Main = false;
            return name;
        }
        public static string ZeroSpace(string name, ParsedObject _, TrieTree<IParserTreeNode> tree)
        {
            new SpaceParserTreeNode(name, 0, tree).Main = false;
            return name;
        }
        public static string Symbol(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new SymbolParserTreeNode(name,obj.Objects.Length==0?(string)obj.Value:$"{obj.Objects[0].Value}", tree).Main = false; ;
            return name;
        }
        public static string SymbolGroup(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new SymbolParserTreeNode(name, GetSymbolGroup(obj), tree).Main = false; ;
            return name;
        }
        public static string Key(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new KeyParserTreeNode(name, GetSymbolGroup(obj), tree).Main = false;
            return name;
        }
        public static string GetSymbolGroup(ParsedObject obj)
        {
            string s = "";
            foreach(ParsedObject value in obj.Objects)
                s +=value.Objects.Length==0?(string)value.Value:$"{value.Objects[0].Value}";
            return s;
        }
        public static string Value(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new ValueParserTreeNode(name, GetSymbolGroup(obj.Objects[0]), true,tree).Main = false; ;
            return name;
        }
        public static string Option(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            if (obj.Objects.Length < 2) throw new System.Exception();
            string[] options = new string[obj.Objects.Length];
            for (int i = 0; i < options.Length; i++)
                options[i] = Parse($"{name}[{i}]", obj.Objects[i], tree);
            new OptionParserTreeNode(name, options, "null", false, tree).Main = false;
            return name;
        }
        public static string Join1(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new JoinParserTreeNode(name, "null", Parse($"{name}.task", obj.Objects[0], tree), 0, int.MaxValue, tree).Main = false;
            return name;
        }
        public static string Join2(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new JoinParserTreeNode(name, Parse($"{name}.sperater", obj.Objects[1], tree), Parse($"{name}.task", obj.Objects[0], tree), 0, int.MaxValue, tree).Main = false;
            return name;
        }
        public static string Join(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            new JoinParserTreeNode(name, "null", Parse($"{name}.sperater", obj.Objects[0], tree), int.Parse(obj.Objects[1].Value as string), int.Parse(obj.Objects[2].Value as string), tree).Main = false;
            return name;
        }
        public static string Stream(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            if (obj.Objects.Length < 2) throw new System.Exception();
            string[] tasks = new string[obj.Objects.Length];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = Parse($"{name}[{i}]", obj.Objects[i], tree);
            new StreamParserTreeNode(name, tasks, tree).Main = false;
            return name;
        }
        public static string EndCheck(string name, ParsedObject obj, TrieTree<IParserTreeNode> tree)
        {
            if (obj.Objects.Length!=0) throw new System.Exception();
            new EndCheckParserTreeNode(name, tree).Main = false;
            return name;
        }
        public static string Print(TrieTree<IParserTreeNode> tree)
        {
            List<string> s = new();
            tree.Foreach((t) =>
            {
                if (t.Value.Main)
                    s.Add(t.Value.Install().ToString());
            });
            return string.Join("\n", s);
        }
    }
}
