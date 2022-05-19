using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.CSharp.Metadata
{
    public class Command_If:ICommand
    {
        public ICommand Condition;
        public ICommand True;
        public ICommand False;
        public Command_If(ICommand condition, ICommand @true, ICommand @false)
        {
            Condition = condition;
            True = @true;
            False = @false;
        }
        public override string ToString()
            => $"{Condition}?{True}:{False}";
    }
}
