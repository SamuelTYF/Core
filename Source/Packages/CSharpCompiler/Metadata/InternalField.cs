using CSharpCompiler.Searching;
using System.Reflection;

namespace CSharpCompiler.Metadata
{
    public class InternalField:IField
    {
        public InternalField(FieldInfo field):base()
        {
            Name = field.Name;
            TypeFullName = field.FieldType.GetFullName();
            Attributes.UnionWith(field.Attributes.ToString().Split(","));
        }
        public void Lock(SearchingResult top) => Type = top.GetType(TypeFullName);
    }
}
