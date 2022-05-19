using Compiler.CSharp.Searching;
using System.Reflection;

namespace Compiler.CSharp.Metadata
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
                p.Attributes.UnionWith(parameter.Attributes.ToString().Split(","));
                Parameters.Add(p);
            }
        }
        public void Lock(SearchingResult top)
        {
            ReturnType = top.GetType(ReturnTypeFullName);
            foreach (Parameter parameter in Parameters)
                parameter.Lock(top);
        }
    }
}