using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CSharp.Metadata
{
    public class Command_Operator2:ICommand
    {
        public ICommand A;
        public ICommand B;
        public string Operator;
        public Command_Operator2(ICommand a, ICommand b, string @operator)
        {
            A = a;
            B = b;
            Operator = @operator;
        }
        public override string ToString() => $"{A}{Operator}{B}";
    }
}
