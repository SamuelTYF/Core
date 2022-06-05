using Compiler.Tokenizor;
using TestFramework;
namespace Compiler.Test
{
    public class Build_CSharp_Tokenizer_String_Test : ITest
    {
        public Build_CSharp_Tokenizer_String_Test()
            :base("Build_CSharp_Tokenizer_String_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            RE re = new();
            re.Register(Properties.Resources.CSharp_String_Token);
            re.Combine();
            string code = re.BuildTokenizer("CSharp_String_Tokenizer","char");
            UpdateInfo(code);
            update(1);
            UpdateInfo(string.Join("\n", re.Errors));
            Ensure.Equal(re.Errors.Count, 0);
            update(2);
        }
    }
}
