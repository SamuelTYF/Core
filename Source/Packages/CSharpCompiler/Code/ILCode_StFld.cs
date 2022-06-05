using CSharpCompiler.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCompiler.Code
{
    public class ILCode_StFld : ILCode
    {
        public IField Field;
        public ILCode_StFld(ILCode_Block parent, IField field) : base(parent)
            => Field = field;
        public override int GetLength() => 1;
        public override string ToString() => $"IL_{Offset}:\tstfld\t{Field}";
    }
}
