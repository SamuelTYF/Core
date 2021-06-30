using Collection.Serialization;
using System.IO;
using TestFramework;
namespace Collection.Test
{
    public class List_Test : ITest
    {
        public List_Test()
            : base("List_Test",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            int len = 10000;
            List<int> a = new();
            for (int i = 0; i < len; i++)
            {
                a.Add(i);
                Ensure.Equal(a.Length, i + 1);
            }
            update(1);
            MemoryStream ms = new();
            using (Formatter formatter = new())
                formatter.Serialize(ms, a);
            update(2);
            ms.Position = 0;
            List<int> b;
            using (Formatter formatter = new())
                b = formatter.Deserialize(ms) as List<int>;
            update(3);
            for (int i = 0; i < len; i++)
                Ensure.Equal(b.Pop(), len - i - 1);
            update(4);
        }
    }
}
