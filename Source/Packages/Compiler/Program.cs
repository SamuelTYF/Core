using Compiler.CSharp;
using Compiler.CSharp.Metadata;
using Compiler.CSharp.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SearchingNode_Root root = new();
            root.LoadAssembly(typeof(string).Assembly);
            root.Lock();
            CSharp_Tokenizer tokenizer = new(Encoding.UTF8);
            tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.Test_Code)));
            CSharp_Parser parser = new();
            ParsingFile file=parser.Parse(tokenizer);
            Console.WriteLine(file);
        }
    }
}
