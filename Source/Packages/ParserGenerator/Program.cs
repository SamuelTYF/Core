using Compiler.Parser;
using Compiler.Tokenizor;
using Net.Json;

if (args.Length != 1)
    Environment.Exit(1);

FileInfo fi=new(args[0]);

if(fi.DirectoryName!=null)Environment.CurrentDirectory = fi.DirectoryName;

string json;
using (StreamReader sr = new(fi.FullName))
    json = sr.ReadToEnd();
Console.WriteLine(json);
ObjectNode? node = Node.Parse(json) as ObjectNode;
if(node==null) Environment.Exit(1);
string? OutputDir = (node["OutputDir"] as StringNode)?.Value;
if (OutputDir == null) Environment.Exit(1);
string? _Language = (node["Language"] as StringNode)?.Value;
Language language = _Language == null ? Language.CSharp : Enum.Parse<Language>(_Language);
ObjectNode? tokenizer = node["Tokenizer"] as ObjectNode;
ObjectNode? parser = node["Parser"] as ObjectNode;
if (tokenizer != null)
{
    string? Source = (tokenizer["Source"] as StringNode)?.Value;
    if(Source==null) Environment.Exit(1);
    string? Method = (tokenizer["Method"] as StringNode)?.Value;
    string? Name = (tokenizer["Name"] as StringNode)?.Value;
    if (Name == null) Environment.Exit(1);
    string? Token = (tokenizer["Token"] as StringNode)?.Value;
    if (Token == null) Environment.Exit(1);
    string?[]? Header = (tokenizer["Header"] as ArrayNode)?.Nodes.Select(n => (n as StringNode)?.Value).ToArray();
    if (Header == null) Environment.Exit(1);
    string?[]? Footer = (tokenizer["Footer"] as ArrayNode)?.Nodes.Select(n => (n as StringNode)?.Value).ToArray();
    if (Footer == null) Environment.Exit(1);
    RE re = new();
    using (StreamReader sr = new(Source))
        re.Register(sr.ReadToEnd());
    re.Combine();
    string method = "";
    if (Method != null)
    using (StreamReader sr = new(Method))
        method = sr.ReadToEnd();
    string result = re.BuildTokenizer(Name, Token, method, language);
    if (re.Errors.Count != 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (string error in re.Errors)
            Console.WriteLine(error);
        Environment.Exit(1);
    }
    switch(language)
    {
        case Language.CSharp or Language.CSharpTyped:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.cs");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                foreach (string r in result.Split("\r\n"))
                {
                    sw.Write('\t');
                    sw.WriteLine(r);
                }
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
            }
            break;
        case Language.Python:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.py");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                foreach (string r in result.Split("\r\n"))
                    sw.WriteLine(r);
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
            }
            break;
        case Language.CPP or Language.CPPshort:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.cpp");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                sw.WriteLine(result);
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
            }
            break;
        default:throw new Exception();
    }
    Console.WriteLine("Tokenizer Generate Success");
}
if (parser != null)
{
    List<string> Sources = new();
    if (parser["Source"] is StringNode sn)
        Sources.Add(sn.Value);
    else if (parser["Source"] is ArrayNode an)
    {
        foreach (StringNode s in an.Nodes)
            Sources.Add(s.Value);
    }
    else Environment.Exit(1);
    if (Sources.Count == 0) Environment.Exit(1);
    string? Method = (parser["Method"] as StringNode)?.Value;
    string? Init = (parser["Init"] as StringNode)?.Value;
    string? Types = (parser["Types"] as StringNode)?.Value;
    string? Name = (parser["Name"] as StringNode)?.Value;
    if (Name == null) Environment.Exit(1);
    string? Token = (parser["Token"] as StringNode)?.Value;
    if (Token == null) Environment.Exit(1);
    string? Value = (parser["Value"] as StringNode)?.Value;
    if (Value == null) Environment.Exit(1);
    string? Result = (parser["Result"] as StringNode)?.Value;
    if (Result == null) Environment.Exit(1);
    string?[]? Header = (parser["Header"] as ArrayNode)?.Nodes.Select(n => (n as StringNode)?.Value).ToArray();
    if (Header == null) Environment.Exit(1);
    string?[]? Footer = (parser["Footer"] as ArrayNode)?.Nodes.Select(n => (n as StringNode)?.Value).ToArray();
    if (Footer == null) Environment.Exit(1);
    LALR lalr = new();
    foreach(string s in Sources)
    {
        using StreamReader grammar = new(s);
        Console.WriteLine($"Adding Source {s}");
        lalr.Register(grammar.ReadToEnd()+"\n");
    }
    lalr.ComputeFirst();
    lalr.CreateClosures();
    if (Types != null) lalr.LoadTypes(Types);
    foreach (var error in lalr.Errors)
        Console.WriteLine(error);
    string method = "";
    string init = "";
    if (Method != null)
        using (StreamReader sr = new(Method))
            method = sr.ReadToEnd();
    if (Init != null)
        using (StreamReader sr = new(Init))
            init = sr.ReadToEnd();
    string result = lalr.BuildParser(Name, Token, Value, Result, method, init, language);
    if(lalr.Errors.Count!=0)
    {
        //Console.ForegroundColor = ConsoleColor.Red;
        //foreach (string error in lalr.Errors)
        //    Console.WriteLine(error);
        Environment.Exit(1);
    }
    
    switch(language)
    {
        case Language.CSharp:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.cs");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                foreach (string r in result.Split("\r\n"))
                {
                    sw.Write('\t');
                    sw.WriteLine(r);
                }
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
            }
            break;
        case Language.CSharpTyped:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.cs");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                foreach (string r in result.Split("\r\n"))
                {
                    sw.Write('\t');
                    sw.WriteLine(r);
                }
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
                using StreamWriter swt = new($"{OutputDir}/{Name}.Types.txt");
                foreach (Symbol s in lalr.Variables)
                    if (s.Type != null)
                        swt.WriteLine($"{s.Name}:{s.Type}");
                    else swt.WriteLine(s.Name);
            }
            break;
        case Language.CPP or Language.CPPshort:
            {
                using StreamWriter sw = new($"{OutputDir}/{Name}.cpp");
                foreach (string? h in Header)
                    if (h != null)
                        sw.WriteLine(h);
                sw.WriteLine(result);
                foreach (string? f in Footer)
                    if (f != null)
                        sw.WriteLine(f);
                using StreamWriter swt = new($"{OutputDir}/{Name}.Types.txt");
                foreach (Symbol s in lalr.Variables)
                    if (s.Type != null)
                        swt.WriteLine($"{s.Name}:{s.Type}");
                    else swt.WriteLine(s.Name);
            }
            break;
        default:throw new NotImplementedException();
    }
    Console.WriteLine("Parser Generate Success");
}