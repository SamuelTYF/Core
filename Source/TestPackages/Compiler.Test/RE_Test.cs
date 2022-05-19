using Compiler.Tokenizor;
using TestFramework;
namespace Compiler.Test
{
    public class RE_Test : ITest
    {
        public RE_Test()
            :base("RE_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            RE re = new();
            re.Register(Properties.Resources.RE_Token);
            re.Combine();
            string code = re.BuildTokenizer("RE_Tokenizer", "Token");
            UpdateInfo(code);
            update(1);
            Ensure.Equal(re.Errors.Count, 0);
        }
    }
}
