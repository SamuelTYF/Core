using Compiler.Parser;
using TestFramework;
namespace Compiler.Test
{
    public class Build_RE_LALR_Test : ITest
    {
        public Build_RE_LALR_Test()
            :base("Build_RE_LALR_Test", 4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            LALR lalr = new();
            lalr.Register(Properties.Resources.RE_LALR);
            Ensure.Equal(lalr.Errors.Count, 0);
            update(1);
            lalr.ComputeFirst();
            Ensure.Equal(lalr.Errors.Count, 0);
            update(2);
            lalr.CreateClosures();
            Ensure.Equal(lalr.Errors.Count, 0);
            update(3);
            string code=lalr.BuildParser("RE_Parser", "Token", "IRE_Block", "Parsed_Result", method:Properties.Resources.RE_LALR_Method,init:Properties.Resources.RE_LALR_Init);
            UpdateInfo(code);
            Ensure.Equal(lalr.Errors.Count, 0);
            update(4);
        }
    }
}
