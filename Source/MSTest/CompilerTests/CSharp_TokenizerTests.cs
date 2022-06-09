using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpCompiler;

namespace CSharpCompiler.Tests
{
    [TestClass()]
    public class CSharp_TokenizerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            CSharp_Tokenizer tokenizer = new(Encoding.UTF8);
            tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(Compiler.Properties.Resources.Test_Code)));
            Token token;
            do token = tokenizer.Get();while (token != null && token.Type != "EOF");
            Assert.IsNotNull(token);
        }
    }
}