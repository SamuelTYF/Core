using CSharpCompiler;
using System.Text;
using TestFramework;
namespace Compiler.Test
{
    public class CSharp_Tokenizer_Test : ITest
    {
        public CSharp_Tokenizer_Test()
            :base("CSharp_Tokenizer_Test", 1)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            CSharp_Tokenizer tokenizer = new(Encoding.UTF8);
            tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.Test_Code)));
            Token token;
            do
            {
                token = tokenizer.Get();
                UpdateInfo(token);
            } while (token != null && token.Type != "EOF");
            UpdateInfo(tokenizer._Error);
            Ensure.NotNull(token);
        }
    }
}
