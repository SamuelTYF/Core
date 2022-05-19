using Compiler.CSharp;
using System.Text;
using TestFramework;
namespace Compiler.Test
{
    public class CSharp_Parser_Test : ITest
    {
        public CSharp_Parser_Test()
            :base("CSharp_Parser_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            CSharp_Tokenizer tokenizer = new(Encoding.UTF8);
            tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.Test_Code)));
            CSharp_Parser parser = new();
            parser.Parse(tokenizer);
        }
    }
}
