using Compiler.Parser;
using TestFramework;
namespace Compiler.Test
{
    public class LALR_Tokenizer_Test : ITest
    {
        public LALR_Tokenizer_Test()
            :base("LALR_Tokenizer_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            LALR_Tokenizer tokenizer = new(System.Text.Encoding.UTF8);
            Delta delta=tokenizer.ParseDelta(@"<Start>-><File>'EOF'");
            UpdateInfo(tokenizer._Error);
            Ensure.NotNull(delta);
            update(1);
            UpdateInfo(delta);
        }
    }
}
