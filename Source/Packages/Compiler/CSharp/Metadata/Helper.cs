namespace Compiler.CSharp.Metadata
{
    public static class Helper
    {
        public static string[] GetFullName(this Type type)
        {
            Stack<string> names = new();
            while (type.IsNested)
            {
                names.Push(type.Name);
                type = type.DeclaringType;
            }
            names.Push(type.Name);
            List<string> t = new();
            t.AddRange(type.Namespace.Split("."));
            t.AddRange(names);
            return t.ToArray();
        }
    }
}
