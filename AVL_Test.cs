using Collection;
using Collection.Serialization;
using System;
using System.IO;
using TestFramework;
using DebugTool;
namespace Collection.Test
{
    public class AVL_Test : ITest
    {
        public AVL_Test()
            :base("AVL_Test")
        {
        }
        public override void Run()
        {
            int len = 10000;
            int[] keys = new int[len];
            double[] values = new double[len];
            int[] indexs = new int[len];
            Random _R = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < len; i++)
            {
                keys[i] = indexs[i] = i;
                values[i] = _R.NextDouble();
            }
            Array.Sort(indexs, (a, b) => values[a].CompareTo(values[b]));
            AVL<int, double> tree = new AVL<int, double>();
            for (int i = 0; i < len; i++) 
            {
                tree[keys[indexs[i]]] = values[indexs[i]];
                Ensure.Equal(i + 1, tree.Length);
            }
            for (int i = 0; i < len; i++)
                Ensure.Equal(tree[keys[i]], values[i]);
            int[] sortkeys = tree.LDROrder();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortkeys[i], i);
            double[] sortvalues = tree.LDRSort();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortvalues[i], values[i]);
            MemoryStream ms = new MemoryStream();
            using (Formatter formatter = new Formatter())
                formatter.Serialize(ms, tree);
            ms.Position = 0;
            AVL<int, double> stree;
            using (Formatter formatter = new Formatter())
                stree = formatter.Deserialize(ms) as AVL<int, double>;
            sortkeys = stree.LDROrder();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortkeys[i], i);
            sortvalues = stree.LDRSort();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortvalues[i], values[i]);
            for(int i=0;i<len;i++)
            {
                stree.Remove(i);
                Ensure.Equal(stree.Length, len - i - 1);
                Ensure.Equal(stree.ContainsKey(i), false);
            }
            DebugForm.Display("AVL", tree);
        }
    }
}
