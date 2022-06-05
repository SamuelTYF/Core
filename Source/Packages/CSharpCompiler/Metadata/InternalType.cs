using CSharpCompiler.Searching;
using System.Reflection;
using System.Xml.Linq;

namespace CSharpCompiler.Metadata
{
    public class InternalType:IType
    {
        public InternalType(Type type):base()
        {
            Name= type.Name;
            BaseTypeFullName = type.BaseType?.GetFullName();
            Attributes.UnionWith(type.Attributes.ToString().Split(","));
            foreach (FieldInfo fi in type.GetFields())
                if (fi.IsPublic)
                {
                    InternalField field = new(fi);
                    Fields[field.Name] = field;
                }
            foreach (PropertyInfo pi in type.GetProperties())
            {
                InternalProperty property = new(pi);
                Properties[property.Name] = property;
            }
            foreach (MethodInfo mi in type.GetMethods())
                if (mi.IsPublic)
                {
                    InternalMethod method = new(mi);
                    if (!Methods.ContainsKey(method.Name))
                        Methods[method.Name] = new();
                    Methods[method.Name].Add(method);
                }
        }
        public void Lock(SearchingResult top)
        {
            foreach (InternalField field in Fields.Values)
                field.Lock(top);
            foreach (InternalProperty property in Properties.Values)
                property.Lock(top);
            foreach (List<IMethod> methods in Methods.Values)
                foreach (InternalMethod method in methods)
                    method.Lock(top);
            IType objecttype = top.GetType("System", "Object");
            BaseType = BaseTypeFullName==null?this== objecttype ?null: objecttype : top.GetType(BaseTypeFullName);
        }
    }
}
