using Compiler.Tokenizor;
using TestFramework;
namespace Compiler.Test
{
    public class Build_LALR_Tokenizer_Test : ITest
    {
        public Build_LALR_Tokenizer_Test()
            :base("Build_LALR_Tokenizer_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            RE re = new();
            re.Register(Properties.Resources.LALR_Token);
            re.Combine();
            string code = re.BuildTokenizer("LALR_Tokenizer", "Token");
            UpdateInfo(code);
            update(1);
            UpdateInfo(string.Join("\n", re.Errors));
            Ensure.Equal(re.Errors.Count, 0);
            update(2);
        }
    }
}
