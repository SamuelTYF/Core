using Compiler.CSharp.Searching;
using System.Reflection;
using System.Xml.Linq;

namespace Compiler.CSharp.Metadata
{
    public class InternalType:IType
    {
        public InternalType(Type type):base()
        {
            Name= type.Name;
            BaseTypeFullName = type.BaseType?.FullName;
            Attributes.UnionWith(type.Attributes.ToString().Split(","));
            foreach (FieldInfo fi in type.GetFields())
                if (fi.IsPublic)
                {
                    InternalField field = new(fi);
                    Fields[field.Name] = field;
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
            foreach (List<IMethod> methods in Methods.Values)
                foreach (InternalMethod method in methods)
                    method.Lock(top);
        }
    }
}
