using Collection.Serialization;
using System.IO;
using TestFramework;

namespace Collection.Test
{
    public class List_Test:ITest
    {
        public List_Test() 
            : base("List_Test") 
        { 
        }
        public override void Run()
        {
            int len = 10000;
            List<int> a = new List<int>();
            for(int i=0;i< len; i++)
            {
                a.Add(i);
                Ensure.Equal(a.Length, i + 1);
            }
            MemoryStream ms = new MemoryStream();
            using (Formatter formatter = new Formatter())
                formatter.Serialize(ms, a);
            ms.Position = 0;
            List<int> b;
            using (Formatter formatter = new Formatter())
                b = formatter.Deserialize(ms) as List<int>;
            for (int i=0;i< len;i++)
                Ensure.Equal(b.Pop(), len - i - 1);
        }
    }
}
