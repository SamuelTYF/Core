using Collection;
using Net.Xml;
using System.Drawing;
using System.Reflection;
namespace Tikz
{
    public class ITikzPackage
    {
        public TrieTree<TikzParser> Parsers;
        public ITikzPackage()
        {
            Parsers = new();
            foreach (MethodInfo mi in GetType().GetRuntimeMethods())
            {
                ParameterInfo[] pis = mi.GetParameters();
                if (mi.IsStatic &&
                    mi.ReturnType == typeof(Bitmap) &&
                    pis.Length == 2 &&
                    pis[0].ParameterType == typeof(XmlNode) &&
                    pis[1].ParameterType==typeof(TikzCommandManager))
                    Parsers[mi.Name] = mi.CreateDelegate<TikzParser>();
            }
        }
    }
}
