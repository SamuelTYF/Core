using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasm
{
    class Parser
    {
        public static void Parse(string s)
        {
            var tree = GrammarParser.GetTree(GrammarParser.GetParsedObject(Properties.Resources.ASM));
            using StringArg arg = new(s);
            IParser parser = tree["@mov16@"].Install();
            IParseResult result = parser.Parse(arg);
        }
    }
}
