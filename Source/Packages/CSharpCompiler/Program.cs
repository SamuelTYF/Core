using System.IO;
using System.Text;
using System;
using CSharpCompiler.Metadata;

namespace CSharpCompiler
{
    public class Program
    {
        static void Main()
        {
            CSharp_Tokenizer tokenizer = new(Encoding.UTF8, 1 << 10);
            using Stream stream = new FileInfo("Test.cs").OpenRead();
            tokenizer.StartParse(stream);
            CSharp_Parser parser = new();
            ParsingFile file = parser.Parse(tokenizer);
            if (file!=null&&file.Errors.Count == 0)
            {
                Console.WriteLine(file);
                List<UserMethod> methods = new();
                foreach (UserType type in file.GetTypes())
                    methods.AddRange(type.GetMethods());
                foreach (UserMethod method in methods)
                    Console.WriteLine(method.ILFormat());
            }
            else if(file!=null)
            {
                foreach (string error in file.Errors)
                    Console.Error.WriteLine(error);
            }
            Console.ReadLine();
        }
    }
}