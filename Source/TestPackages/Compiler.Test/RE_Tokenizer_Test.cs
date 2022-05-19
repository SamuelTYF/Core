using Compiler.Tokenizor;
using System.Text;
using TestFramework;
namespace Compiler.Test
{
    public class RE_Tokenizer_Test : ITest
    {
        public string Value = @"(|\-)[0-9]+";
        public MemoryStream Stream;
        public RE_Tokenizer_Test()
            :base("RE_Tokenizor_Test", 1)
        {
            Stream = new(Encoding.UTF8.GetBytes(Value));
        }
        public override void Run(UpdateTaskProgress update)
        {
            RE_Tokenizer tokenizer = new();
            tokenizer.StartParse(Stream);
            Token token;
            do
            {
                token = tokenizer.Get();
                UpdateInfo(token);
            } while (token != null && token.Type != "EOF");
        }
    }
}
