using Compiler.Parser;
using Compiler.Tokenizor;
using TestFramework;
namespace Compiler.Test
{
    public class Build_CSharp_Parser_Test : ITest
    {
        public Build_CSharp_Parser_Test()
            :base("Build_CSharp_Parser_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            LALR lalr = new();
            lalr.Register(Properties.Resources.CSharp_LALR);
            Ensure.Equal(lalr.Errors.Count, 0);
            update(1);
            lalr.ComputeFirst();
            Ensure.Equal(lalr.Errors.Count, 0);
            update(2);
            lalr.CreateClosures();
            Ensure.Equal(lalr.Errors.Count, 0);
            update(3);
            string code = lalr.BuildParser("CSharp_Parser", "Token", "object", "ParsingFile",Properties.Resources.CSharp_LALR_Method,Properties.Resources.CSharp_LALR_Init);
            UpdateInfo(code);
            Ensure.Equal(lalr.Errors.Count, 0);
            update(4);
        }
    }
}
