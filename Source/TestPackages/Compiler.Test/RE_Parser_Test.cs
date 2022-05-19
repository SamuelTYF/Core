using Compiler.Tokenizor;
using Compiler.Tokenizor.Automata;
using System;
using System.IO;
using System.Text;
using TestFramework;
namespace Compiler.Test
{
    public class RE_Parser_Test : ITest
    {
        public RE_Parser_Test()
            :base("RE_Parser_Test", 1)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            RE_Tokenizer tokenizer = new();
            tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(@"(|\-)[0-9]+")));
            RE_Parser parser = new();
            Parsed_Result result = parser.Parse(tokenizer);
            UpdateInfo(result);
            update(1);
        }
    }
}
