using Compiler.Parser;

LALR lalr = new();
using StreamReader grammar = new("Grammar.txt");
lalr.Register(grammar.ReadToEnd());
grammar.Dispose();
lalr.ComputeFirst();
lalr.CreateClosures();
foreach (var error in lalr.Errors)
    Console.WriteLine(error);
using StreamWriter l = new("lalr.txt");
foreach (var delta in lalr.Deltas)
    l.WriteLine(delta);
l.Dispose();
using StreamReader method = new("Method.txt");
using StreamReader init = new("Init.txt");
string code = lalr.BuildParser("CSharp_Parser", "Token", "object", "ParsingFile", method.ReadToEnd(), init.ReadToEnd());
using StreamWriter sw = new(@"D:\Core\Source\Packages\CSharpCompiler\CSharp_Parser.cs");
sw.WriteLine("using CSharpCompiler.Metadata;");
sw.WriteLine("using CSharpCompiler.Searching;");
sw.WriteLine("using Compiler;");
sw.WriteLine();
sw.WriteLine("namespace CSharpCompiler");
sw.WriteLine("{");
foreach(var line in code.Replace("\r","").Split("\n"))
    sw.WriteLine("\t"+line);
sw.WriteLine("}");
sw.Dispose();
if(lalr.Errors.Count>0)
    Console.ReadLine();