
using CSharpCompiler.Searching;
using System.Reflection;

namespace CSharpCompiler.Metadata
{
    public class InternalProperty:IProperty
    {
        public InternalProperty(PropertyInfo property) : base()
        {
            Name = property.Name;
            TypeFullName = property.PropertyType.GetFullName();
            Attributes.UnionWith(property.Attributes.ToString().Split(","));
            if(property.SetMethod!=null)
                Setter = new InternalMethod(property.SetMethod);
            if (property.GetMethod != null)
                Getter = new InternalMethod(property.GetMethod);
        }
        public void Lock(SearchingResult top)
        {
            Type = top.GetType(TypeFullName);
            if (Setter != null) (Setter as InternalMethod).Lock(top);
            if (Getter != null) (Getter as InternalMethod).Lock(top);
        }
    }
}
