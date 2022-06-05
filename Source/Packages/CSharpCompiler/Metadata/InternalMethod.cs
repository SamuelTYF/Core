using CSharpCompiler.Searching;
using System.Reflection;

namespace CSharpCompiler.Metadata
{
    public class InternalMethod:IMethod
    {
        public InternalMethod(MethodInfo method)
        {
            ReturnTypeFullName = method.ReturnType.GetFullName();
            Name = method.Name;
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                Parameter p = new(parameter.ParameterType.GetFullName(), parameter.Name);
                p.Attributes.UnionWith(parameter.Attributes.ToString().Split(", "));
                Parameters.Add(p);
            }
            Attributes.UnionWith(method.Attributes.ToString().ToLower().Split(", "));
        }
        public void Lock(SearchingResult top)
        {
            ReturnType = top.GetType(ReturnTypeFullName);
            foreach (Parameter parameter in Parameters)
                parameter.Lock(top);
        }
    }
}