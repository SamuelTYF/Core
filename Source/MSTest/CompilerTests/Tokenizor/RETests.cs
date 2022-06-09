using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compiler.Tokenizor.Tests
{
    [TestClass()]
    public class RETests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            RE re = new();
            re.Register(Properties.Resources.RE_Token);
            re.Combine();
            string code = re.BuildTokenizer("RE_Tokenizer", "Token");
            Assert.AreEqual(re.Errors.Count, 0);
        }
        [TestMethod]
        public void BuildCSharpTokenTest()
        {
            RE re = new();
            re.Register(Properties.Resources.CSharp_Token);
            re.Combine();
            string code = re.BuildTokenizer("CSharp_Tokenizer", "Token", Properties.Resources.CSharp_Token_Method);
            Assert.AreEqual(re.Errors.Count, 0);
        }
        [TestMethod]
        public void BuildCSharpStringTokenTest()
        {
            RE re = new();
            re.Register(Properties.Resources.CSharp_String_Token);
            re.Combine();
            string code = re.BuildTokenizer("CSharp_String_Tokenizer", "char");
            Assert.AreEqual(re.Errors.Count, 0);
        }
    }
}